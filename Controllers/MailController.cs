using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using apiSupplier.Interceptor;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiSupplier.Entities;
using System.Linq;
using ProblemDetails = apiSupplier.Entities.ProblemDetails;
using NotFoundResult = apiSupplier.Entities.NotFoundResult;
using apiSupplier.Models;

namespace apiSupplier.Controllers
{
    [TypeFilter(typeof(InterceptorLogAttribute))]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MailController : Controller
    {
        private msMailClient _clientMsMail;
        private msNotificacionesClient _clientMsNotificacion;
        private msTipoClient _clientMsTipo;
        public MailController(msMailClient clientMsMail, msNotificacionesClient clientMsNotificacion, msTipoClient clientMsTipo)
        {
            _clientMsMail = clientMsMail;
            _clientMsNotificacion = clientMsNotificacion;
            _clientMsTipo = clientMsTipo;
        }

        [HttpPost("MailSend")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MailDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<OutputMail>>> MailSend(string mail, string nombre, int id_tipo_notificacion)
        {
            try
            {
                var tipoCorreos = await _clientMsTipo.TipoNotificacionGetAllAsync();
                var miTipoCorreo = tipoCorreos.First(tn => tn.IdTipoNotificacion == id_tipo_notificacion);
                var notificacionCorreo = await _clientMsNotificacion.NotificacionGetAllAsync();
                var miNotificacionCorreo = notificacionCorreo.First(tn => tn.IdTipoNotificacion == id_tipo_notificacion && tn.IdNotificacionTemplateEmail != null);
                var templateCorreo = await _clientMsNotificacion.NotificacionTemplateEmailGetAllAsync();
                var miTemplateCorreo = templateCorreo.First(tn => tn.IdNotificacionTemplateEmail == miNotificacionCorreo.IdNotificacionTemplateEmail);

                MailDTO miCorreo = CreaMail(mail, nombre, miTipoCorreo, miTemplateCorreo);
                var entidades = await _clientMsMail.SendAsync(miCorreo);
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        [HttpPost("MailSendFormated")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MailDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<OutputMail>>> MailSendFormated( MailFormatDto entrada)
        {
            try
            {
                MailDTO miCorreo = CreaMail(entrada.mail, entrada.nombre, entrada.body, entrada.subject);
                var entidades = await _clientMsMail.SendFormatedAsync(miCorreo);
                if (entidades == null) return NotFound();
                return Ok(entidades);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        private MailDTO CreaMail(string mail, string nombre, string body, string subject)
        {
            if (string.IsNullOrWhiteSpace(mail)) return null;
            try
            {
                var inputMail = new MailDTO
                {
                    From = "mail@OpeN.cl",
                    FromAlias = "OpeN",
                    To = new[] { mail },
                    ToAlias = new[] { nombre },
                    IsHtml = true,
                    Subject = subject,
                    Body = body
                };
                return inputMail;
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
        private MailDTO CreaMail(string mail, string nombre, TipoNotificacionDto miTipoCorreo, NotificacionTemplateEmailDto template)
        {
            if (string.IsNullOrWhiteSpace(mail)) return null;           
            try
            {
                var inputMail = new MailDTO
                {
                    From = "mail@OpeN.cl",
                    FromAlias = "OpeN",
                    To = new[] { mail },
                    ToAlias = new[] { nombre },
                    IsHtml = true,
                    Subject = miTipoCorreo.Nombre,
                    Body = getbodyMail(nombre,miTipoCorreo, template)
                };
                return inputMail;
            }
            catch (System.Exception e)
            {
                throw;
            }
        }
        private static string getbodyMail(string nombre,TipoNotificacionDto miTipoCorreo, NotificacionTemplateEmailDto template)
        {
            return template.Descripcion.Replace("&nombre_cliente", nombre).Replace("&nombre_titulo",miTipoCorreo.Nombre);
//            return "<table style=\"max-width: 500px; padding: 10px; margin:0 auto; border-collapse: collapse;\">"
//+ " 	<tr>"
//+ " 		<td style=\"background-color: #ecf0f1\">"
//+ " 			<div style=\"color: #34495e; margin: 2% 2% 2%; text-align: justify;font-family: sans-serif\">"
//+ " 				<h2 style=\"color: #e67e22; margin: 0 0 3px;font-size: 12px;\">¡Ayuda a un amigo a encontrar una mejor manera de trabajar!</h2>"
//+ " 				<p style=\"margin: 2px; font-size: 10px\">"
//+ " 					Un referido de un miembro de Homework puede generar grandes recompensas para ti. Comparte con otros la experiencia Homework y empieza a disfrutar de grandes beneficios.</p>"
//+ " "
//+ " 				<div style=\"width: 100%;margin:0; display: inline-block;text-align: center\">"
//+ " 					<img style=\"padding: 0; width: 30px; margin: 3px\" src=\"https://img1.wsimg.com/isteam/ip/8844e4ad-6f84-4875-88b9-745b79c44c6d/recomineda-homework.png/:/cr=t:0%25,l:0%25,w:89.29%25,h:89.29%25/rs=w:365,h:365,cg:true,m\">"
//+ " 					<img style=\"padding: 0; width: 30px; margin: 3px\" src=\"https://img1.wsimg.com/isteam/ip/8844e4ad-6f84-4875-88b9-745b79c44c6d/Espera-homework.png/:/cr=t:7.63%25,l:7.63%25,w:84.75%25,h:84.75%25/rs=w:547.5,h:547.5,cg:true\">"
//+ " 					<img style=\"padding: 0; width: 30px; margin: 3px\" src=\"https://img1.wsimg.com/isteam/ip/8844e4ad-6f84-4875-88b9-745b79c44c6d/gana-homework.png/:/cr=t:0%25,l:0%25,w:100%25,h:100%25/rs=w:547.5,h:547.5,cg:true\">"
//+ " 				</div>"
//+ " 				<div style=\"width: 100%; text-align: center; font-size: 8px;\">"
//+ " 					<a style=\"text-decoration: none; border-radius: 5px; padding: 8px 17px; color: white; background-color: #3498db\" href=\"https://homework.com.mx/programa-de-referidos\">Así funciona</a>	"
//+ " 				</div>"
//+ " 				<p style=\"color: #b3b3b3; font-size: 8px; text-align: center;margin: 8px 0 0\">© 2021 Copyright Homework</p>"
//+ " 			</div>"
//+ " 		</td>"
//+ " 	</tr>"
//+ " </table>";
          /*    return   "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0//EN\"> "
                   + " <meta http-equiv=\"content-type\" content=\"text/html; charset=CODIFICACION DE CARACTERES\">"
                   + "<html xmlns=\"http://www.w3.org/1999/xhtml\">"
                   + "<head>"
                   + "<meta name=\"robots\" content=\"noindex, nofollow\">"
                   + "<meta name=\"googlebot\" content=\"noindex, nofollow\">"
                   + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">"
                   + "<title>OpeN</title>"
                   + "<style type=\"text/css\">"
                   + "a:link {"
                   + "color: #2BA6CB;"
                   + "font-weight: bold;"
                   + "text-decoration: none;}"
                   + "A:hover  {	"
                   + "text-decoration: underline;}"
                   + ".Estilo1 {color: #666666}"
                   + "</style>"
                   + "</head>"
                   + ""
                   +
                   "<body style=\"background: #f1f1f1;	font: normal small Arial, Helvetica, sans-serif; margin:0; padding:0\" text=\"#333333\">"
                   +
                   "<table width=\"598\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" height=\"30\">"
                   + "<tr>"
                   +
                   "<td width=\"598\" height=\"30\"align=\"center\" valign=\"top\"><br /><font color=\"#666666\" size=\"1\" face=\"Arial, Helvetica, sans-serif\">"

                   + "</td>"
                   + "  </tr>"
                   + "</table>"
                   +
                   "<table align=\"center\" width=\"610\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"border:1px solid #adadad;\">"

                   + "<tr bgcolor=\"#FFFFFF\">"
                   +
                   "<td style=\"border-top:2px solid #f7f7f7; padding-top:10px; padding-right:10px; padding-bottom:10px; padding-left:10px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"580\"  align=\"center\">"
                   + "<tr>"
                   +
                   "<td width=\"580\" style=\"font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif;font-size:26px; font-style:bold;color: #001d2f;text-align: center;font-variant:small-caps;\"><strong> " + miTipoCorreo.Nombre + "</strong></td>"
                   + "</tr>"
                   + "<tr>"
                   +
                   "<td width=\"580\" style=\"font-family:Arial, Helvetica, sans-serif; font-size:13px;color:#666666; line-height:25px; text-align:justify; vertical-align:middle; padding-top:10px; padding-right:15px; padding-bottom:10px; padding-left:15px;\">"
                   + mensaje
                   + "</td>"
                   + "</tr>"
                   + "</table>"

                   + "<td bgcolor=\"#FFFFFF\">"
                   +
                   "<table width=\"250\" height=\"30px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"background-color:#FFFFFF; padding-top:5px; padding-bottom:5px; font-family:Arial, Helvetica, sans-serif\">"

                   + "</table>"
                   + "</table>"
                   + "<table width=\"610px\" border=\"0\" align=\"center\" cellpadding=\"1\" cellspacing=\"1\">"
                   + "<tr>"
                   +
                   "<td align=\"center\" valign=\"baseline\" bgcolor=\"#f1f1f1\"><font color=\"#666666\" size=\"1\" face=\"Arial, Helvetica, sans-serif\">&copy;OpeN by Homework, Direcci&oacute;n de Desarrollo Web<BR>"
                   + "</font></td>"
                   + "</tr>"
                   //+ "<tr>"
                   //+
                   //"<td align=\"center\" valign=\"baseline\" bgcolor=\"#f1f1f1\"><font color=\"#666666\" size=\"1\" face=\"Arial, Helvetica, sans-serif\">Si no deseas recibir m&aacute;s informaci&oacute;n de nuestra universidad, </font><a href=\"mailto:remover@unab.cl\" target=\"_blank\"><font color=\"#333333\" size=\"1\" face=\"Arial, Helvetica, sans-serif\">clickea aqu&iacute;</font></a><font color=\"#666666\" size=\"1\" face=\"Arial, Helvetica, sans-serif\">,<br />"
                   //+
                   //" y escribe en el   asunto eliminar e inmediatamente tu nombre ser&aacute; borrado de la lista.</font></td>"
                   //+ "</tr>"
                   + "</table>"

                   + "<map name=\"Map\" id=\"Map\">"
                   + "<area shape=\"rect\" coords=\"131,9,235,51\" href=\"https://homework.com.mx\" target=\"_blank\" />"
                   + "</map>"

                   +
                   "<map name=\"Map2\" id=\"Map2\"><area shape=\"rect\" coords=\"132,12,237,50\" href=\"https://homework.com.mx\" target=\"_blank\" />"
                   + "</map></body>"
                   + "</html>";*/

        }
    }
}

To install this tool simply run: dotnet tool install --global Microsoft.dotnet-openapi --version 3.1.1


dotnet openapi add url http://localhost:90/msproducto/swagger/v1/swagger.json --output-file openapi/msProducto.json msProductoClient.cs
dotnet openapi add url http://localhost:91/msmembresia/swagger/v1/swagger.json --output-file openapi/msMembresia.json msMembresiaClient.cs
dotnet openapi add url http://localhost:92/mspaquete/swagger/v1/swagger.json --output-file openapi/msPaquete.json msPaqueteClient.cs
dotnet openapi add url http://localhost:93/mssala/swagger/v1/swagger.json --output-file openapi/msSala.json msSalaClient.cs
dotnet openapi add url http://localhost:94/mstipo/swagger/v1/swagger.json --output-file openapi/mstipo.json mstipoClient.cs
dotnet openapi add url http://localhost:98/swagger/v1/swagger.json --output-file openapi/msSearch.json msSearchClient.cs

dotnet openapi add url https://micro-dev.bmlabs.cl/mstransaccion/swagger/v1/swagger.json --output-file openapi/msTransaccion.json msTransaccionClient.cs



	<ItemGroup>
		<OpenApiReference Include="openapi/msTransaccion.json" SourceUrl="https://micro-dev.bmlabs.cl/mstransaccion/swagger/v1/swagger.json" />
	</ItemGroup>

	<ItemGroup>
		<OpenApiReference Include="openapi/msSearch.json" SourceUrl=http://localhost:98/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msUsuario.json" SourceUrl=https://micro-dev.bmlabs.cl/msUsuario/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msMembresia.json" SourceUrl="http://localhost:91/msmembresia/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msPaquete.json" SourceUrl="http://localhost:92/mspaquete/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msSala.json" SourceUrl="http://localhost:93/mssala/swagger/v1/swagger.json" />
	</ItemGroup>
		<ItemGroup>
		<OpenApiReference Include="openapi/msTipo.json" SourceUrl="http://localhost:94/mstipo/swagger/v1/swagger.json" />
	</ItemGroup>

dotnet build
	/*******************************************************************************/



dotnet openapi add url https://micro-dev.bmlabs.cl/msproducto/swagger/v1/swagger.json --output-file openapi/msProducto.json msProductoClient.cs
dotnet openapi add url https://micro-prod.bmlabs.cl/mssala/swagger/v1/swagger.json --output-file openapi/msSala.json msSalaClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/msmembresia/swagger/v1/swagger.json --output-file openapi/msMembresia.json msMembresiaClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mstipo/swagger/v1/swagger.json --output-file openapi/mstipo.json msTipoClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mspaquete/swagger/v1/swagger.json --output-file openapi/msPaquete.json msPaqueteClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mstransaccion/swagger/v1/swagger.json --output-file openapi/msTransaccion.json msTransaccionClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/msnotificaciones/swagger/v1/swagger.json --output-file openapi/msNotificaciones.json msNotificacionesClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mscalificacion/swagger/v1/swagger.json --output-file openapi/msCalificacion.json msCalificacionClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mssearch/swagger/v1/swagger.json --output-file openapi/msSearch.json msSearchClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mstipo/swagger/v1/swagger.json --output-file openapi/msTipo.json msTipoClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/msOpenPay/swagger/v1/swagger.json --output-file openapi/msOpenPay.json msOpenPayClient.cs
dotnet openapi add url https://micro-dev.bmlabs.cl/mssearch/swagger/v1/swagger.json --output-file openapi/msSearch.json msSearchClient.cs



dodotnet openapi add url https://micro-dev.bmlabs.cl/mstipo/swagger/v1/swagger.json --output-file openapi/mstipo.json msTipoClient.cs

	<ItemGroup>
		<OpenApiReference Include="openapi/msTipo.json" SourceUrl="https://micro-dev.bmlabs.cl/mstipo/swagger/v1/swagger.json" />
	</ItemGroup>



	<ItemGroup>
		<OpenApiReference Include="openapi/msSearch.json" SourceUrl="https://micro-dev.bmlabs.cl/mssearch/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msOpenPay.json" SourceUrl="https://micro-dev.bmlabs.cl/msOpenPay/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msTipo.json" SourceUrl="https://micro-dev.bmlabs.cl/mstipo/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msSearch.json" SourceUrl="https://micro-dev.bmlabs.cl/mssearch/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msCalificacion.json" SourceUrl="https://micro-dev.bmlabs.cl/mscalificacion/swagger/v1/swagger.json" />
	</ItemGroup>
<ItemGroup>
		<OpenApiReference Include="openapi/msTransaccion.json" SourceUrl="https://micro-dev.bmlabs.cl/mstransaccion/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msNotificaciones.json" SourceUrl="https://micro-dev.bmlabs.cl/msNotificaciones/swagger/v1/swagger.json" />
	</ItemGroup>
			<ItemGroup>
		<OpenApiReference Include="openapi/msProducto.json" SourceUrl="https://micro-dev.bmlabs.cl/msproducto/swagger/v1/swagger.json" />
	</ItemGroup>



		<ItemGroup>
		<OpenApiReference Include="openapi/msSala.json" SourceUrl="https://micro-prod.bmlabs.cl/mssala/swagger/v1/swagger.json" />
	</ItemGroup>
	<ItemGroup>
		<OpenApiReference Include="openapi/msMembresia.json" SourceUrl="https://micro-dev.bmlabs.cl/msmembresia/swagger/v1/swagger.json" />
	</ItemGroup>
		<ItemGroup>
		<OpenApiReference Include="openapi/msTipo.json" SourceUrl="https://micro-dev.bmlabs.cl/mstipo/swagger/v1/swagger.json" />
	</ItemGroup>

	<ItemGroup>
		<OpenApiReference Include="openapi/msPaquete.json" SourceUrl="https://micro-dev.bmlabs.cl/mspaquete/swagger/v1/swagger.json" />
	</ItemGroup>

dotnet build


dotnet openapi add url https://micro-dev.bmlabs.cl/mstransaccion/swagger/v1/swagger.json --output-file openapi/msTransaccion.json msTransaccion.cs


	<ItemGroup>
		<OpenApiReference Include="openapi/msTransaccion.json" SourceUrl="https://micro-dev.bmlabs.cl/mstransaccion/swagger/v1/swagger.json" />
	</ItemGroup>

dotnet build
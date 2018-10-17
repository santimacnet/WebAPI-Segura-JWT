# WebAPI-Segura-JWT
Proyecto WebAPI para securitar API REST mediante JWT con Secret-Key y expiración.

![](https://github.com/santimacnet/WebAPI-Segura-JWT/blob/master/Meetup-Barcelona-Foto.jpg)


Se adjuntan las diapositivas PPT del evento en el proyecto.

MEETUP-BCN: https://www.meetup.com/es-ES/En-Mi-Local-Funciona-Barcelona/events/254915418/

## Fundamentos de JWT

Entrando en materia, partimos de la base que conocemos JTW y su ciclo de vida, sino es asi, visitar: https://jwt.io/introduction

![](https://cdn.auth0.com/content/jwt/jwt-diagram.png)


## Articulos Tecnicos en blog atSistemas

Para entender mejor este ejemplo, aqui dejo links a los articulos:

http://enmilocalfunciona.io/construyendo-una-web-api-rest-segura-con-json-web-token-en-net-parte-i/
http://enmilocalfunciona.io/construyendo-una-web-api-rest-segura-con-json-web-token-en-net-parte-ii/
http://enmilocalfunciona.io/construyendo-una-web-api-rest-segura-con-json-web-token-en-net-parte-iii/

## Implementación de JWT

Este ejemplo, sirve como plantilla de referencia para la implementacion de JWT en aplicaciones ASP.NET Web API

En el codigo fuente, veremos como procesar un Token JWT cuando un usuario hace login se genera y entrega el Token JWT 
y en cada petición del usuario, será nuestra responsibilidad cuando recibamos el Token en nuestra WebAPI, 
verificarlo y garantizar que la firma es válida para aceptarlo o denegarlo y eso lo hacemos mediante la libreria 
oficial recomendada por Microsoft: https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt que la propia [JWT libraries](https://jwt.io/#libraries-io) ya pone a nuestra disposición.



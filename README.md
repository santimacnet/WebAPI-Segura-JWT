# WebAPI-Segura-JWT
Proyecto WebAPI para securitar API REST mediante JWT con Secret-Key y expiración.

## Fundamentos de JWT

Entrando en materia, partimos de la base que conocemos JTW y su ciclo de vida, sino es asi, visitar: https://jwt.io/introduction

![](https://cdn.auth0.com/content/jwt/jwt-diagram.png)

## Implementación de JWT

Este ejemplo, sirve como plantilla de referencia para la implementacion de JWT en aplicaciones ASP.NET Web API

En el codigo fuente, veremos como procesar un Token JWT cuando un usuario hace login se genera y entrega el Token JWT 
y en cada petición del usuario, será nuestra responsibilidad cuando recibamos el Token en nuestra WebAPI, 
verificarlo y garantizar que la firma es válida para aceptarlo o denegarlo y eso lo hacemos mediante la libreria 
oficial recomendada por Microsoft: https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt que la propia [JWT libraries](https://jwt.io/#libraries-io) ya pone a nuestra disposición.



Español

Para ejecutar la aplicacion primero hay que correr el proyecto para que la URL del API quede expuesta.

Una vez teniendo el proyecto corriendo, el API se puede validar en el tester que Visual Studio tiene, o bien en algun tester independiente yo lo valide en el tester de Visual Studio y en Postman.

Primero lo que hice fue colocar un limite de solicitudes de 100 historias principales para garantizar un rendimiento y evitar una sobrecarga.
Asi mismo considero que ejecutar solicitudes paralelas aceleran la obtencion de los detalles, y el almacenamiento de cache evita llamadas innecesarias.
Decidi estructurar la aplicacion dividiendo entre Controladores, Interfaces, Servicios y DTO's para poder entender de mejor manera la aplicacion.

Alguna mejora que podria hacer es que la(s) URL's parametrizarlas por si en algun momento habria que hacer alguna modificacion, esto lo haria mediante un archivo de configuracion o bien mediante una conexion hacia alguina base de datos.

--------------------
English
To run the application, you must first run the project so that the API URL is exposed.

Once the project is running, the API can be validated in the Visual Studio tester, or in a separate tester. I validate it in the Visual Studio tester and in Postman.

First, I set a request limit of 100 main stories to ensure performance and avoid overload.
I also considered that executing parallel requests speeds up the retrieval of details, and caching avoids unnecessary calls.
I decided to structure the application by dividing it into Controllers, Interfaces, Services, and DTOs to better understand the application.

One improvement I could make is to parameterize the URL(s) in case any changes were needed at some point. This would be done through a configuration file or through a connection to a database.
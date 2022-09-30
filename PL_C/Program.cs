// See https://aka.ms/new-console-template for more information

ReadFile();


static void ReadFile()
{
    string file = @"C:\Users\Alien8\Documents\Brandon Axell Antonio Franco\BAntonioProgramacionNCapasCore\Usuariodatos.txt";

    if (File.Exists(file))
    {
        ML.Result Errores = new ML.Result();
        StreamReader Textfile = new StreamReader(file);
        string line;
        line= Textfile.ReadLine();
        while ((line = Textfile.ReadLine()) != null)
        {
            String[] lines = line.Split('|');

            ML.Usuario usuario = new ML.Usuario();
            usuario.UserName = lines[0];
            usuario.Nombre = lines[1];
            usuario.ApellidoPaterno = lines[2];
            usuario.ApellidoMaterno = lines[3];
            usuario.Email = lines[4];
            usuario.Password = lines[5];
            usuario.FechaNacimiento = lines[6];
            usuario.Sexo = lines[7];
            usuario.Telefono = lines[8];
            usuario.Celuar = lines[9];
            usuario.Curp = lines[10];

            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = int.Parse(lines[11]);

            usuario.Imagen = lines[12];
            

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[13];
            usuario.Direccion.NumeroInterior = lines[14];
            usuario.Direccion.NumeroExterior = lines[15];

            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.IdColonia = int.Parse(lines[16]);

            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();


                Errores.Objects.Add(
                  "Fallo al insertar el UserName" + usuario.UserName +
                  "Fallo al insertar el Nombre" + usuario.Nombre +
                  "Fallo al insertar el Apellido Paterno" + usuario.ApellidoPaterno +
                  "Fallo al insertar el Apellido Materno" + usuario.ApellidoMaterno +
                  "Fallo al insertar el Email" + usuario.Email +
                  "Fallo al insertar el Password" + usuario.Password +
                  "Fallo al insertar el Fecha Nacimiento" + usuario.FechaNacimiento +
                  "Fallo al insertar el Sexo" + usuario.Sexo +
                  "Fallo al insertar el Telefono" + usuario.Telefono +
                  "Fallo al insertar el Celular" + usuario.Celuar +
                  "Fallo al insertar el CURP" + usuario.Curp +
                  "Fallo al insertar la Imagen" + usuario.Imagen +

                  "Fallo al insertar la Calle" + usuario.Direccion.Calle +
                  "Fallo al insertar el Numero Interior" + usuario.Direccion.NumeroInterior +
                  "Fallo al insertar el Numero Exterior" + usuario.Direccion.NumeroExterior +

                  "Fallo al insertar el IdDireccion" + usuario.Direccion.IdDireccion
                                    );
               

            }
            else
            {
                Console.WriteLine("Registro añadido exitosamente");
                Console.ReadKey();
                
            }

            System.IO.TextWriter writeFile = new StreamWriter(@"C:\Users\Alien8\Documents\Brandon Axell Antonio Franco\BAntonioProgramacionNCapasCore\Errores.txt");
            foreach (string error in Errores.Objects)
            {
                writeFile.WriteLine(error);
            }
            writeFile.Close();
        }
    }
}
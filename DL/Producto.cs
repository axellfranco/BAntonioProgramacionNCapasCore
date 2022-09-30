﻿using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdDepartamento { get; set; }
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual Proveedor? IdProveedorNavigation { get; set; }

        //Proveedor
        public string? NombreProveedor { get; set; }


        //Departamento

        public string? NombreDepartamento { get; set; }

    }
}

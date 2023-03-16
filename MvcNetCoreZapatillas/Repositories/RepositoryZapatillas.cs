using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//CREATE PROCEDURE SP_IMAGEN_ZAPATILLA_PAGINAR
//(@POSICION INT, @IDPRODUCTO INT)
//AS
//    SELECT idImagen, idProducto, imagen FROM
//        (SELECT CAST(
//            ROW_NUMBER() OVER(ORDER BY imagen) AS INT) AS POSICION,
//            idImagen, idProducto, imagen
//        FROM IMAGENESZAPASPRACTICA
//        WHERE idProducto = @IDPRODUCTO) AS QUERY
//    WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION < (@POSICION + 1)
//GO
#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatilla()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> GetZapatilla(int idproducto)
        {
            Zapatilla zapatilla = await this.context.Zapatillas.
                FirstOrDefaultAsync(z => z.IdProducto == idproducto);
            return zapatilla;
        }

        public int GetNumeroImagenZapatilla(int idproducto)
        {
            return this.context.ImagenesZapatillas.
                Where(z => z.IdProducto == idproducto).Count();
        }

        public async Task<List<ImagenZapatilla>>
        GetImagenZapatillaAsync(int posicion, int idproducto)
        {
            string sql =
                "SP_IMAGEN_ZAPATILLA_PAGINAR @POSICION, @IDPRODUCTO";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamidimagen =
                new SqlParameter("@IDPRODUCTO", idproducto);

            var consulta =
                this.context.ImagenesZapatillas.FromSqlRaw(sql, pamposicion, pamidimagen);
            List<ImagenZapatilla> zapatillas = await consulta.ToListAsync();

            return zapatillas;
        }

       


    }
}

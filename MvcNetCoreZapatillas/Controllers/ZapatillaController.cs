using Microsoft.AspNetCore.Mvc;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    public class ZapatillaController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillaController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetZapatillas()
        {
            List<Zapatilla> zapatillas =  await this.repo.GetZapatilla();
            return View(zapatillas);
        }



        public async Task<IActionResult> DetailsZapatilla(int idproducto)
        {
            Zapatilla zapatilla = await this.repo.GetZapatilla(idproducto);
            return View(zapatilla);
        }

        public async Task<IActionResult> ImagenZapatilla(int? posicion, int idproducto)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            List<ImagenZapatilla> imagenzapatilla =
                    await this.repo.GetImagenZapatillaAsync(posicion.Value, idproducto);
            ViewData["REGISTROS"] = this.repo.GetNumeroImagenZapatilla(idproducto);
            ViewData["IDPRODUCTO"] = idproducto;
            return View(imagenzapatilla);
        }

        public async Task<IActionResult> _DetailsS(int idproducto)
        {

            Zapatilla zapatilla = await this.repo.GetZapatilla(idproducto);
            return PartialView("_DetailsS", zapatilla);
        }


        
      


    }
}

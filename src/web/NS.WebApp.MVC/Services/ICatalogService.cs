using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIdAsync(Guid id);
    }

    //public interface ICatalogServiceRefit
    //{
    //    [Get("/catalogo/produtos/")]
    //    Task<IEnumerable<ProductViewModel>> GetAllAsync();

    //    [Get("/catalogo/produtos/{id}")]
    //    Task<ProductViewModel> GetByIdAsync(Guid id);
    //}
}
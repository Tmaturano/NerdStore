using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NS.Basket.API.Data;
using NS.Basket.API.Models;
using NS.WebApi.Core.Controllers;
using NS.WebApi.Core.User;

namespace NS.Basket.API.Controllers
{
    [Route("api/basket")]
    [Authorize]
    public class BasketController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly BasketContext _context;

        public BasketController(IAspNetUser user, BasketContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("basket")]
        public async Task<BasketClient> Get()
        {
            return null;
            //return await ObterCarrinhoCliente() ?? new CarrinhoCliente();
        }

        [HttpPost("basket")]
        public async Task<IActionResult> Post(BasketItem item)
        {
            //var carrinho = await ObterCarrinhoCliente();

            //if (carrinho == null)
            //    ManipularNovoCarrinho(item);
            //else
            //    ManipularCarrinhoExistente(carrinho, item);

            //if (!OperacaoValida()) return CustomResponse();

            //await PersistirDados();
            return CustomResponse();
        }

        [HttpPut("basket/{productId}")]
        public async Task<IActionResult> Put(Guid productId, BasketItem item)
        {
            //var carrinho = await ObterCarrinhoCliente();
            //var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho, item);
            //if (itemCarrinho == null) return CustomResponse();

            //carrinho.AtualizarUnidades(itemCarrinho, item.Quantidade);

            //ValidarCarrinho(carrinho);
            //if (!OperacaoValida()) return CustomResponse();

            //_context.CarrinhoItens.Update(itemCarrinho);
            //_context.CarrinhoCliente.Update(carrinho);

            //await PersistirDados();
            return CustomResponse();
        }

        [HttpDelete("basket/{productId}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            return CustomResponse();
            //var carrinho = await ObterCarrinhoCliente();

            //var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho);
            //if (itemCarrinho == null) return CustomResponse();

            //ValidarCarrinho(carrinho);
            //if (!OperacaoValida()) return CustomResponse();

            //carrinho.RemoverItem(itemCarrinho);

            //_context.CarrinhoItens.Remove(itemCarrinho);
            //_context.CarrinhoCliente.Update(carrinho);

            //await PersistirDados();
            //return CustomResponse();
        }

        //private async Task<CarrinhoCliente> ObterCarrinhoCliente()
        //{
        //    return await _context.CarrinhoCliente
        //        .Include(c => c.Itens)
        //        .FirstOrDefaultAsync(c => c.ClienteId == _user.ObterUserId());
        //}
        //private void ManipularNovoCarrinho(CarrinhoItem item)
        //{
        //    var carrinho = new CarrinhoCliente(_user.ObterUserId());
        //    carrinho.AdicionarItem(item);

        //    ValidarCarrinho(carrinho);
        //    _context.CarrinhoCliente.Add(carrinho);
        //}
        //private void ManipularCarrinhoExistente(CarrinhoCliente carrinho, CarrinhoItem item)
        //{
        //    var produtoItemExistente = carrinho.CarrinhoItemExistente(item);

        //    carrinho.AdicionarItem(item);
        //    ValidarCarrinho(carrinho);

        //    if (produtoItemExistente)
        //    {
        //        _context.CarrinhoItens.Update(carrinho.ObterPorProdutoId(item.ProdutoId));
        //    }
        //    else
        //    {
        //        _context.CarrinhoItens.Add(item);
        //    }

        //    _context.CarrinhoCliente.Update(carrinho);
        //}
        //private async Task<CarrinhoItem> ObterItemCarrinhoValidado(Guid produtoId, CarrinhoCliente carrinho, CarrinhoItem item = null)
        //{
        //    if (item != null && produtoId != item.ProdutoId)
        //    {
        //        AdicionarErroProcessamento("O item não corresponde ao informado");
        //        return null;
        //    }

        //    if (carrinho == null)
        //    {
        //        AdicionarErroProcessamento("Carrinho não encontrado");
        //        return null;
        //    }

        //    var itemCarrinho = await _context.CarrinhoItens
        //        .FirstOrDefaultAsync(i => i.CarrinhoId == carrinho.Id && i.ProdutoId == produtoId);

        //    if (itemCarrinho == null || !carrinho.CarrinhoItemExistente(itemCarrinho))
        //    {
        //        AdicionarErroProcessamento("O item não está no carrinho");
        //        return null;
        //    }

        //    return itemCarrinho;
        //}
        //private async Task PersistirDados()
        //{
        //    var result = await _context.SaveChangesAsync();
        //    if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        //}
        //private bool ValidarCarrinho(CarrinhoCliente carrinho)
        //{
        //    if (carrinho.EhValido()) return true;

        //    carrinho.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
        //    return false;
        //}
    }
}

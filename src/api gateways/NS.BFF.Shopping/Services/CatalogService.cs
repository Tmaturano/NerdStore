﻿using Microsoft.Extensions.Options;
using NS.Bff.Shopping.Services;
using NS.BFF.Shopping.Extensions;
using NS.BFF.Shopping.Models;

namespace NSE.Bff.Shopping.Services
{
    public interface ICatalogService
    {
        Task<ProductItemDTO> GetById(Guid id);
    }

    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }

        public async Task<ProductItemDTO> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<ProductItemDTO>(response);
        }
    }
}
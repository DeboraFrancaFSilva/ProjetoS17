﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //2. o Index vai ter que chamar nossa operação FindAll do SellerService, 1o fazer a dependência
        private readonly SellerService _sellerService;

        //3.Para injetar a dependência fazer o construtor
        public SellersController (SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        //1.Por padrão criou Index, para testar essa ação, vamos criar uma pagina de Index,a view, vá a pasta View para criar.
        public IActionResult Index()
        {
            //4.Agora para chamar e passar como argumento na View, para gerar IActionResult contendo a lista
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Entao assim: No MVC, eu chamo o controlador "Index" que acessou meu Model "var list = _sellerService.FindAll();"
        //pegou o dado na "list" e vai encaminhar esses dados para a "View" essa é a dinamica MVC acontecendo. 

        //IActionResult é o tipo de retorno de TODAS as ações
        public IActionResult Create()
        {
            return View();//simplesmente vai retornar a View Create
        }

        [HttpPost]//como essa ação "Create(Seller seller)" é um POST preciso indicar fazendo isso "[HttpPost]"
        [ValidateAntiForgeryToken] //Isso serve para proteger contra ataques CSRF, ataques maliciosos.
        public IActionResult Create(Seller seller)//no parametro recebe e instanicia um objeto vendedor que veio da requisição,
        {
            _sellerService.Insert(seller); //Ação para inserir no banco da dados
            return RedirectToAction(nameof(Index)); //após inserir vou redirecionar a ação para o método Index, para voltar a tela principal.
            //nameof colocamos porque se um dia eu mudar o name "index" por outra palavra não vou precisar mudar aqui embaixo tb. 

        }


    }
}
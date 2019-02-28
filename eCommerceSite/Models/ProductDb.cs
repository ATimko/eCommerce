﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Models
{
    public static class ProductDb
    {
        public static Product Add(Product p, CommerceContext db)
        {
            db.Products.Add(p);
            db.SaveChanges();
            return p;
        }

        public static List<Product> GetProducts(CommerceContext context)
        {
            //LINQ Query Syntax
            return (from p in context.Products
                    select p).ToList();

            //LINQ Method Syntax
            //return context.Products.ToList();
        }

        public static Product GetProduct(CommerceContext context, int id)
        {
            //LINQ method syntax - grab product by id
            Product p2 = context
                            .Products
                            .Where(product => product.ProductId == id)
                            .Single();
            return p2;

            //LINQ query syntax - Grabbing a single product by id
            //Product p = (from prods in context.Products
            //             where prods.ProductId == id
            //             select prods).Single();

            //return p;
        }

        public static List<Product> GetProductsByPage
            (CommerceContext context, int pageNum, int pageSize)
        {
            int pageOffset = 1;

            //the page number must be offset to get the correct
            //page of products
            int numRecordsToSkip = (pageNum - pageOffset) * pageSize;

            //MAKE SURE SKIP IS CALLED BEFORE TAKE!!!!!!!!!!!!!!
            return context
                        .Products
                        .OrderBy(p => p.Name)
                        .Skip(numRecordsToSkip)
                        .Take(pageSize)
                        .ToList();
        }

        /// <summary>
        /// Returns the total number of pages needed
        /// to display all products given the pageSize
        /// </summary>
        /// <param name="content">The database context</param>
        /// <param name="pageSize">The num of products per page</param>
        /// <returns></returns>
        public static int GetMaxPage
            (CommerceContext context, int pageSize)
        {
            int numProducts = (from p in context.Products
                               select p).Count();
            double totalPagesPartial = (double)numProducts / pageSize;

            return (int)Math.Ceiling(totalPagesPartial);
        }
    }
}

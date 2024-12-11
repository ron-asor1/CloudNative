var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<List<Product>>();
var app = builder.Build();

SeedData(app.Services.GetRequiredService<List<Product>>());

app.MapGet("/api/products", (List<Product> products) => {
    return Results.Ok(products);
});

app.MapGet("/api/products/{id}", (int id, List<Product> products) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null){
        return Results.NotFound();
    }
    return Results.Ok(product);
});

app.MapPost("/api/products", (Product product, List<Product> products) => {
    products.Add(product);
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id}", (int id, Product updatedProduct ,List<Product> products) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null){
        return Results.NotFound();
    }
    product.Name = updatedProduct.Name;
    product.Price = updatedProduct.Price;
    return Results.NoContent();
});

app.MapDelete("/api/products/{id}", (int id,List<Product> products) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product == null){
        return Results.NotFound();
    }

    products.Remove(product);
    return Results.NoContent();
});

app.Run();

static void SeedData(List<Product> products){
    products.AddRange(new List<Product>{
        new Product {Id = 1, Name = "Product A", Price = 12.99m},
        new Product {Id = 2, Name = "Product B", Price = 15.99m},
        new Product {Id = 3, Name = "Product C", Price = 18.99m},
        new Product {Id = 4, Name = "Product D", Price = 21.99m},
    });
}



using Microsoft.EntityFrameworkCore;
using WebApplication5.database;
using WebApplication5.model;


var builder = WebApplication.CreateBuilder();
string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseStatusCodePages();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db) => await db.Users.ToListAsync());

app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    // получаем пользовател€ по id
    Person? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, отправл€ем его
    return Results.Json(user);
});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    // получаем пользовател€ по id
    Person? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    // если не найден, отправл€ем статусный код и сообщение об ошибке   
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, удал€ем его
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPost("/api/users", async (Person user, ApplicationContext db) =>
{
    // добавл€ем пользовател€ в массив
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});

app.MapPut("/api/users", async (Person userData, ApplicationContext db) =>
{
    // получаем пользовател€ по id
    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    // если пользователь найден, измен€ем его данные и отправл€ем обратно на клиент
    user.Age = userData.Age;
    user.Name = userData.Name;
    await db.SaveChangesAsync();
    return Results.Json(user);
});


app.MapGet("/api/kards", async (ApplicationContext db) => await db.Kards.ToListAsync());

app.MapGet("/api/kards/{id:int}", async (int id, ApplicationContext db) =>
{
    // получаем карту по id
    Karta? kart = await db.Kards.FirstOrDefaultAsync(u => u.Id == id);

    // если не найдена, отправл€ем статусный код и сообщение об ошибке
    if (kart == null) return Results.NotFound(new { message = "—кидочна€ карта не найдена" });

    // если карта найдена выводим
    return Results.Json(kart);
});

app.MapDelete("/api/kards/{id:int}", async (int id, ApplicationContext db) =>
{
    // получаем карту по id
    Karta? kart = await db.Kards.FirstOrDefaultAsync(u => u.Id == id);

    // если не найдена, отправл€ем статусный код и сообщение об ошибке   
    if (kart == null) return Results.NotFound(new { message = "—кидочан€ карта не найдена" });

    // если карта найдена, удал€ем ее
    db.Kards.Remove(kart);
    await db.SaveChangesAsync();
    return Results.Json(kart);
});

app.MapPost("/api/kards", async (Karta kart, ApplicationContext db) =>
{
    // добавл€ем карту в массив
    await db.Kards.AddAsync(kart);
    await db.SaveChangesAsync();
    return kart;
});

app.MapPut("/api/kards", async (Karta kartData, ApplicationContext db) =>
{
    // получаем карту по id
    var kart = await db.Kards.FirstOrDefaultAsync(u => u.Id == kartData.Id);

    // если не найдена, отправл€ем статусный код и сообщение об ошибке
    if (kart == null) return Results.NotFound(new { message = "—кидочан€ карта не найдена" });

    // если скидочна€ карта найдена, измен€ем его данные и отправл€ем обратно на сайт
    kart.NomerKarty = kartData.NomerKarty;
    kart.Skidka = kartData.Skidka;
    kart.IdPerson = kartData.IdPerson;
    await db.SaveChangesAsync();
    return Results.Json(kart);
});


app.Run();
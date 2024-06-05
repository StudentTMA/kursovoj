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
    // �������� ������������ �� id
    Person? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ���������� ���
    return Results.Json(user);
});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    // �������� ������������ �� id
    Person? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������   
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, ������� ���
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPost("/api/users", async (Person user, ApplicationContext db) =>
{
    // ��������� ������������ � ������
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});

app.MapPut("/api/users", async (Person userData, ApplicationContext db) =>
{
    // �������� ������������ �� id
    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

    // ���� ������������ ������, �������� ��� ������ � ���������� ������� �� ������
    user.Age = userData.Age;
    user.Name = userData.Name;
    await db.SaveChangesAsync();
    return Results.Json(user);
});


app.MapGet("/api/kards", async (ApplicationContext db) => await db.Kards.ToListAsync());

app.MapGet("/api/kards/{id:int}", async (int id, ApplicationContext db) =>
{
    // �������� ����� �� id
    Karta? kart = await db.Kards.FirstOrDefaultAsync(u => u.Id == id);

    // ���� �� �������, ���������� ��������� ��� � ��������� �� ������
    if (kart == null) return Results.NotFound(new { message = "��������� ����� �� �������" });

    // ���� ����� ������� �������
    return Results.Json(kart);
});

app.MapDelete("/api/kards/{id:int}", async (int id, ApplicationContext db) =>
{
    // �������� ����� �� id
    Karta? kart = await db.Kards.FirstOrDefaultAsync(u => u.Id == id);

    // ���� �� �������, ���������� ��������� ��� � ��������� �� ������   
    if (kart == null) return Results.NotFound(new { message = "��������� ����� �� �������" });

    // ���� ����� �������, ������� ��
    db.Kards.Remove(kart);
    await db.SaveChangesAsync();
    return Results.Json(kart);
});

app.MapPost("/api/kards", async (Karta kart, ApplicationContext db) =>
{
    // ��������� ����� � ������
    await db.Kards.AddAsync(kart);
    await db.SaveChangesAsync();
    return kart;
});

app.MapPut("/api/kards", async (Karta kartData, ApplicationContext db) =>
{
    // �������� ����� �� id
    var kart = await db.Kards.FirstOrDefaultAsync(u => u.Id == kartData.Id);

    // ���� �� �������, ���������� ��������� ��� � ��������� �� ������
    if (kart == null) return Results.NotFound(new { message = "��������� ����� �� �������" });

    // ���� ��������� ����� �������, �������� ��� ������ � ���������� ������� �� ����
    kart.NomerKarty = kartData.NomerKarty;
    kart.Skidka = kartData.Skidka;
    kart.IdPerson = kartData.IdPerson;
    await db.SaveChangesAsync();
    return Results.Json(kart);
});


app.Run();
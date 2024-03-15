using TMAWarehouse.Web.Services;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IItemService, ItemService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();

SD.ItemAPIBase = builder.Configuration["ServiceUrls:ItemAPI"];
SD.ItemAPIBase = builder.Configuration["ServiceUrls:OrderAPI"];


builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore.SqlServer;
using Product_Inventory.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register AdminRepo with dependency injection
builder.Services.AddScoped<AdminRepo>();
builder.Services.AddScoped<UserRepo>();
builder.Services.AddSession(options =>
{
    // Configure session options
    options.Cookie.Name = "ProductInventoryApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
                                                    
});
builder.Services.AddHttpContextAccessor();
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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*dotnet ef dbcontext scaffold "Server=localhost,1433;Database=tempdb;User Id=sa;Password=lahore@17;" Microsoft.EntityFrameworkCore.SqlServer -o Models
 * 
 *                                 @foreach (var product in @Model)
                                {
                                    <div class="d-flex flex-column">
                                        <form class="myForm">

                                            <div>
                                                <span class="text-dark font-weight-bold ms-sm-3">Name: <input type="text" value="@product.Name" readonly></span>
                                            </div>
                                            <div>
                                                <span class="mb-2 text-xs">Quantity: <input type="text" value="@product.Quantity" readonly></span>
                                            </div>
                                            <div>
                                                <span class="mb-2 text-xs">Price: <input type="text" value="@product.Price" readonly></span>
                                            </div>
                                            <div>
                                                <span class="text-xs">Category: <input type="text" value="@product.Category" readonly></span>
                                            </div>

                                            <!-- Delete button -->
                                            <div class="text-end">
                                                <a class="btn btn-link text-danger text-gradient px-3 mb-0 deleteButton" href="javascript:;" onclick="handleDeleteButtonClick(event)" style="display: none;"><i class="far fa-trash-alt me-2"></i>Delete</a>
                                            </div>
                                        </form>
                                    </div>
                                }



    

*/
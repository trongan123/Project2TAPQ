using BusinessObjects.Models;
using DataAccess.Repositories.IService;
using DataAccess.Repositories.Service;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddControllers().AddOData(option => option.Select().Filter()
          .Count().OrderBy().Expand().SetMaxTop(106).AddRouteComponents("odata", GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICooperativeRoomService, CooperativeRoomService>();
builder.Services.AddTransient<IDetailReceiptsPaymentService, DetailReceiptsPaymentService>();
builder.Services.AddTransient<IDistrictService, DistrictService>();
builder.Services.AddTransient<IFishCategoryService, FishCategoryService>();
builder.Services.AddTransient<IHistoryStoreHouseService, HistoryStoreHouseService>();
builder.Services.AddTransient<IItemCategoryService, ItemCategoryService>();
builder.Services.AddTransient<IItemStoreHouseService, ItemStoreHouseService>();
builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IPondDiaryService, PondDiaryService>();
builder.Services.AddTransient<IPondService, PondService>();
builder.Services.AddTransient<IProvinceService, ProvinceService>();
builder.Services.AddTransient<IQuantityHouseService, QuantityHouseService>();
builder.Services.AddTransient<IReceiptsPaymentService, ReceiptsPaymentService>();
builder.Services.AddTransient<IRoleStaffService, RoleStaffService>();
builder.Services.AddTransient<IStoreHouseService, StoreHouseService>();
builder.Services.AddTransient<IWardService, WardService>();
builder.Services.AddTransient<INotifyService, NotifyService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseODataBatching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Account>("Accounts");
    builder.EntitySet<CooperativeRoom>("CooperativeRooms");
    builder.EntitySet<DetailReceiptsPayment>("DetailReceiptsPayments");
    builder.EntitySet<District>("Districts");
    builder.EntitySet<FishCategory>("FishCategories");
    builder.EntitySet<HistoryStoreHouse>("HistoryStoreHouses");
    builder.EntitySet<ItemCategory>("ItemCategories");
    builder.EntitySet<ItemStoreHouse>("ItemStoreHouses");
    builder.EntitySet<Member>("Members");
    builder.EntitySet<Pond>("Ponds");
    builder.EntitySet<PondDiary>("PondDiaries");
    builder.EntitySet<Province>("Provinces");
    builder.EntitySet<QuantityHouse>("QuantityHouses");
    builder.EntitySet<ReceiptsPayment>("ReceiptsPayments");
    builder.EntitySet<RoleStaff>("RoleStaffs");
    builder.EntitySet<StoreHouse>("StoreHouses");
    builder.EntitySet<Ward>("Wards");



    return builder.GetEdmModel();

}
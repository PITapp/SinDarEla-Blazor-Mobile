using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SinDarElaMobile.Data;
using SinDarElaMobile.Models;
using SinDarElaMobile.Authentication;

using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

namespace SinDarElaMobile
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAny",
                    x =>
                    {
                        x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials();
                    });
            });
            var oDataBuilder = new ODataConventionModelBuilder();

            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.AbrechnungBasis>("AbrechnungBases");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.AbrechnungKundenReststunden>("AbrechnungKundenReststundens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Aufgaben>("Aufgabens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.AuswahlJahr>("AuswahlJahrs");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.AuswahlMonat>("AuswahlMonats");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Base>("Bases");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.BaseAnreden>("BaseAnredens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.BaseKontakte>("BaseKontaktes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Benutzer>("Benutzers");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Debugg>("Debuggs");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.DeviceCode>("DeviceCodes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Dokumente>("Dokumentes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.DokumenteKategorien>("DokumenteKategoriens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Ereignisse>("Ereignisses");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.EreignisseArten>("EreignisseArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.EreignisseSonderurlaubArten>("EreignisseSonderurlaubArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.EreignisseTeilnehmer>("EreignisseTeilnehmers");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.EreignisseTeilnehmerStatus>("EreignisseTeilnehmerStatuses");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Feedback>("Feedbacks");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.InfotexteHtml>("InfotexteHtmls");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Key>("Keys");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Kunden>("Kundens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenKontakte>("KundenKontaktes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenKontakteArten>("KundenKontakteArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungArten>("KundenLeistungArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungen>("KundenLeistungens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungenBescheide>("KundenLeistungenBescheides");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungenBescheideKontingente>("KundenLeistungenBescheideKontingentes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungenBescheideStatus>("KundenLeistungenBescheideStatuses");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungenBetreuer>("KundenLeistungenBetreuers");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenLeistungenBetreuerArten>("KundenLeistungenBetreuerArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.KundenStatus>("KundenStatuses");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Mitarbeiter>("Mitarbeiters");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterArten>("MitarbeiterArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterFortbildungen>("MitarbeiterFortbildungens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterFortbildungenArten>("MitarbeiterFortbildungenArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterKundenbudget>("MitarbeiterKundenbudgets");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterKundenbudgetKategorien>("MitarbeiterKundenbudgetKategoriens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterStatus>("MitarbeiterStatuses");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterTaetigkeiten>("MitarbeiterTaetigkeitens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterTaetigkeitenArten>("MitarbeiterTaetigkeitenArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterUrlaub>("MitarbeiterUrlaubs");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterUrlaubDetail>("MitarbeiterUrlaubDetails");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterUrlaubKumuliertAnspruch>("MitarbeiterUrlaubKumuliertAnspruches");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterUrlaubKumuliertDienstzeiten>("MitarbeiterUrlaubKumuliertDienstzeitens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterVerlaufDienstzeiten>("MitarbeiterVerlaufDienstzeitens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterVerlaufDienstzeitenArten>("MitarbeiterVerlaufDienstzeitenArtens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterVerlaufGehalt>("MitarbeiterVerlaufGehalts");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitarbeiterVerlaufNormalarbeitszeit>("MitarbeiterVerlaufNormalarbeitszeits");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Mitteilungen>("Mitteilungens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.MitteilungenVerteiler>("MitteilungenVerteilers");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Notizen>("Notizens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.PersistedGrant>("PersistedGrants");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.Protokoll>("Protokolls");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.RegelnAbwesenheiten>("RegelnAbwesenheitens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwBaseAlle>("VwBaseAlles");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwBaseKontakte>("VwBaseKontaktes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwBaseOrte>("VwBaseOrtes");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwBasePlz>("VwBasePlzs");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwBenutzerBase>("VwBenutzerBases");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwBenutzerRollen>("VwBenutzerRollens");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwKundenBetreuer>("VwKundenBetreuers");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwKundenUndBetreuerAuswahl>("VwKundenUndBetreuerAuswahls");
            oDataBuilder.EntitySet<SinDarElaMobile.Models.DbSinDarEla.VwRollen>("VwRollens");

            this.OnConfigureOData(oDataBuilder);

            oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
            var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
            usersType.AddCollectionProperty(typeof(ApplicationUser).GetProperty("RoleNames"));
            oDataBuilder.EntitySet<IdentityRole>("ApplicationRoles");

            var model = oDataBuilder.GetEdmModel();
            services.AddControllers().AddOData(opt => { 
              opt.AddRouteComponents("odata/dbSinDarEla", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
              opt.AddRouteComponents("auth", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
            });

            

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                  options.UseMySql(Configuration.GetConnectionString("dbSinDarElaConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("dbSinDarElaConnection")));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddRoleStore<RoleStore<IdentityRole, ApplicationIdentityDbContext, string>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();
            services.AddTransient<Duende.IdentityServer.Services.IProfileService, ProfileService>();
            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddDbContext<SinDarElaMobile.Data.DbSinDarElaContext>(options =>
            {
              options.UseMySql(Configuration.GetConnectionString("dbSinDarElaConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("dbSinDarElaConnection")));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfigureOData(ODataConventionModelBuilder builder);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationIdentityDbContext identityDbContext)
        {
            OnConfiguring(app, env);
            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    ctx.Request.Scheme = "https";
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
            app.UseCors("AllowAny");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            identityDbContext.Database.Migrate();

            OnConfigure(app, env);
        }
    }


     public class ProfileService : Duende.IdentityServer.Services.IProfileService
    {
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.env = env;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(Duende.IdentityServer.Models.ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            var roles = user != null ? await userManager.GetRolesAsync(user) :
                env.IsDevelopment() && context.Subject.Identity.Name == "admin" ?
                    roleManager.Roles.Select(r => r.Name) : Enumerable.Empty<string>();

            context.IssuedClaims.AddRange(roles.Select(r => new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Role, r)));
        }

        public Task IsActiveAsync(Duende.IdentityServer.Models.IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}

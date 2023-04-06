using Microsoft.AspNetCore.Identity;
using NetKubernates.Models;

namespace NetKubernates.Data;

public class LoadDatabase {
    public static async Task InsertData(AppDbContext context, UserManager<Usuario> userManager){
        
        
        if(!userManager.Users.Any()){

            var usuario= new Usuario{
                Name= "Irving",
                LastName= "Martinez",
                Email= "irving.martinez@gmail.com",
                UserName="IMTZL",
                Phone="12345678"
            };

            await userManager.CreateAsync(usuario, "IMTZLPWD");
        }

        if(!context.Inmuebles!.Any()){
            context.Inmuebles!.AddRange( new Inmueble{
                Name="Casa de Playa",
                Address="Manzano 10 Casa Blanca",
                Price=750000,
                CreationDate= DateTime.Now
            },
            new Inmueble{
                Name="Casa de Descanso",
                Address="Manzano 44 Casa Blanca",
                Price=500000,
                CreationDate= DateTime.Now
            });
        }
    }
}
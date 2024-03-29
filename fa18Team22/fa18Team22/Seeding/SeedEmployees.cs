using fa18Team22.Models;
using fa18Team22.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace fa18Team22.Seeding
{
	public static class SeedEmployees
	{
		public static async System.Threading.Tasks.Task SeedAllEmployeesAsync(AppDbContext db, IServiceProvider serviceProvider)
		{
			UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			if (db.Users.Count() == 28)
			{
				throw new NotSupportedException("The database already contains all 28 employees!");
			}

			Int32 intEmployeesAdded = 0;
			String employeeEmail = "Begin"; //helps to keep track of error on repos
			List<AppUser> AppUsers = new List<AppUser>();

			try
			{
				AppUser u1 = new AppUser();
				u1.UserName = "c.baker@bevosbooks.com";
				u1.Email = "c.baker@bevosbooks.com";
				u1.PhoneNumber = "3395325649";
				u1.FirstName = "Christopher";
				u1.LastName = "Baker";
				u1.Address = "1245 Lake Libris Dr.";
				u1.City = "Cedar Park";
				u1.State = "TX";
				u1.Zip = "78613";
				u1.UserStatus = "Active";
				IdentityResult resultu1 = await _userManager.CreateAsync(u1,"dewey4");
				if(resultu1.Succeeded)
				{
					await _userManager.AddToRoleAsync(u1,"Manager");
				}
				AppUsers.Add(u1);

				AppUser u2 = new AppUser();
				u2.UserName = "s.barnes@bevosbooks.com";
				u2.Email = "s.barnes@bevosbooks.com";
				u2.PhoneNumber = "9636389416";
				u2.FirstName = "Susan";
				u2.LastName = "Barnes";
				u2.Address = "888 S. Main";
				u2.City = "Kyle";
				u2.State = "TX";
				u2.Zip = "78640";
				u2.UserStatus = "Active";
				IdentityResult resultu2 = await _userManager.CreateAsync(u2,"smitty");
				if(resultu2.Succeeded)
				{
					await _userManager.AddToRoleAsync(u2,"Employee");
				}
				AppUsers.Add(u2);

				AppUser u3 = new AppUser();
				u3.UserName = "h.garcia@bevosbooks.com";
				u3.Email = "h.garcia@bevosbooks.com";
				u3.PhoneNumber = "4547135738";
				u3.FirstName = "Hector";
				u3.LastName = "Garcia";
				u3.Address = "777 PBR Drive";
				u3.City = "Austin";
				u3.State = "TX";
				u3.Zip = "78712";
				u3.UserStatus = "Active";
				IdentityResult resultu3 = await _userManager.CreateAsync(u3,"squirrel");
				if(resultu3.Succeeded)
				{
					await _userManager.AddToRoleAsync(u3,"Employee");
				}
				AppUsers.Add(u3);

				AppUser u4 = new AppUser();
				u4.UserName = "b.ingram@bevosbooks.com";
				u4.Email = "b.ingram@bevosbooks.com";
				u4.PhoneNumber = "5817343315";
				u4.FirstName = "Brad";
				u4.LastName = "Ingram";
				u4.Address = "6548 La Posada Ct.";
				u4.City = "Austin";
				u4.State = "TX";
				u4.Zip = "78705";
				u4.UserStatus = "Active";
				IdentityResult resultu4 = await _userManager.CreateAsync(u4,"changalang");
				if(resultu4.Succeeded)
				{
					await _userManager.AddToRoleAsync(u4,"Employee");
				}
				AppUsers.Add(u4);

				AppUser u5 = new AppUser();
				u5.UserName = "j.jackson@bevosbooks.com";
				u5.Email = "j.jackson@bevosbooks.com";
				u5.PhoneNumber = "8241915317";
				u5.FirstName = "Jack";
				u5.LastName = "Jackson";
				u5.Address = "222 Main";
				u5.City = "Austin";
				u5.State = "TX";
				u5.Zip = "78760";
				u5.UserStatus = "Active";
				IdentityResult resultu5 = await _userManager.CreateAsync(u5,"rhythm");
				if(resultu5.Succeeded)
				{
					await _userManager.AddToRoleAsync(u5,"Employee");
				}
				AppUsers.Add(u5);

				AppUser u6 = new AppUser();
				u6.UserName = "t.jacobs@bevosbooks.com";
				u6.Email = "t.jacobs@bevosbooks.com";
				u6.PhoneNumber = "2477822475";
				u6.FirstName = "Todd";
				u6.LastName = "Jacobs";
				u6.Address = "4564 Elm St.";
				u6.City = "Georgetown";
				u6.State = "TX";
				u6.Zip = "78628";
				u6.UserStatus = "Active";
				IdentityResult resultu6 = await _userManager.CreateAsync(u6,"approval");
				if(resultu6.Succeeded)
				{
					await _userManager.AddToRoleAsync(u6,"Employee");
				}
				AppUsers.Add(u6);

				AppUser u7 = new AppUser();
				u7.UserName = "l.jones@bevosbooks.com";
				u7.Email = "l.jones@bevosbooks.com";
				u7.PhoneNumber = "4764966462";
				u7.FirstName = "Lester";
				u7.LastName = "Jones";
				u7.Address = "999 LeBlat";
				u7.City = "Austin";
				u7.State = "TX";
				u7.Zip = "78747";
				u7.UserStatus = "Active";
				IdentityResult resultu7 = await _userManager.CreateAsync(u7,"society");
				if(resultu7.Succeeded)
				{
					await _userManager.AddToRoleAsync(u7,"Employee");
				}
				AppUsers.Add(u7);

				AppUser u8 = new AppUser();
				u8.UserName = "b.larson@bevosbooks.com";
				u8.Email = "b.larson@bevosbooks.com";
				u8.PhoneNumber = "3355258855";
				u8.FirstName = "Bill";
				u8.LastName = "Larson";
				u8.Address = "1212 N. First Ave";
				u8.City = "Round Rock";
				u8.State = "TX";
				u8.Zip = "78665";
				u8.UserStatus = "Active";
				IdentityResult resultu8 = await _userManager.CreateAsync(u8,"tanman");
				if(resultu8.Succeeded)
				{
					await _userManager.AddToRoleAsync(u8,"Employee");
				}
				AppUsers.Add(u8);

				AppUser u9 = new AppUser();
				u9.UserName = "v.lawrence@bevosbooks.com";
				u9.Email = "v.lawrence@bevosbooks.com";
				u9.PhoneNumber = "7511273054";
				u9.FirstName = "Victoria";
				u9.LastName = "Lawrence";
				u9.Address = "6639 Bookworm Ln.";
				u9.City = "Austin";
				u9.State = "TX";
				u9.Zip = "78712";
				u9.UserStatus = "Active";
				IdentityResult resultu9 = await _userManager.CreateAsync(u9,"longhorns");
				if(resultu9.Succeeded)
				{
					await _userManager.AddToRoleAsync(u9,"Employee");
				}
				AppUsers.Add(u9);

				AppUser u10 = new AppUser();
				u10.UserName = "m.lopez@bevosbooks.com";
				u10.Email = "m.lopez@bevosbooks.com";
				u10.PhoneNumber = "7477907070";
				u10.FirstName = "Marshall";
				u10.LastName = "Lopez";
				u10.Address = "90 SW North St";
				u10.City = "Austin";
				u10.State = "TX";
				u10.Zip = "78729";
				u10.UserStatus = "Active";
				IdentityResult resultu10 = await _userManager.CreateAsync(u10,"swansong");
				if(resultu10.Succeeded)
				{
					await _userManager.AddToRoleAsync(u10,"Employee");
				}
				AppUsers.Add(u10);

				AppUser u11 = new AppUser();
				u11.UserName = "j.macleod@bevosbooks.com";
				u11.Email = "j.macleod@bevosbooks.com";
				u11.PhoneNumber = "2621216845";
				u11.FirstName = "Jennifer";
				u11.LastName = "Macleod";
				u11.Address = "2504 Far West Blvd.";
				u11.City = "Austin";
				u11.State = "TX";
				u11.Zip = "78705";
				u11.UserStatus = "Active";
				IdentityResult resultu11 = await _userManager.CreateAsync(u11,"fungus");
				if(resultu11.Succeeded)
				{
					await _userManager.AddToRoleAsync(u11,"Employee");
				}
				AppUsers.Add(u11);

				AppUser u12 = new AppUser();
				u12.UserName = "e.markham@bevosbooks.com";
				u12.Email = "e.markham@bevosbooks.com";
				u12.PhoneNumber = "5028075807";
				u12.FirstName = "Elizabeth";
				u12.LastName = "Markham";
				u12.Address = "7861 Chevy Chase";
				u12.City = "Austin";
				u12.State = "TX";
				u12.Zip = "78785";
				u12.UserStatus = "Active";
				IdentityResult resultu12 = await _userManager.CreateAsync(u12,"median");
				if(resultu12.Succeeded)
				{
					await _userManager.AddToRoleAsync(u12,"Employee");
				}
				AppUsers.Add(u12);

				AppUser u13 = new AppUser();
				u13.UserName = "g.martinez@bevosbooks.com";
				u13.Email = "g.martinez@bevosbooks.com";
				u13.PhoneNumber = "1994708542";
				u13.FirstName = "Gregory";
				u13.LastName = "Martinez";
				u13.Address = "8295 Sunset Blvd.";
				u13.City = "Austin";
				u13.State = "TX";
				u13.Zip = "78712";
				u13.UserStatus = "Active";
				IdentityResult resultu13 = await _userManager.CreateAsync(u13,"decorate");
				if(resultu13.Succeeded)
				{
					await _userManager.AddToRoleAsync(u13,"Employee");
				}
				AppUsers.Add(u13);

				AppUser u14 = new AppUser();
				u14.UserName = "j.mason@bevosbooks.com";
				u14.Email = "j.mason@bevosbooks.com";
				u14.PhoneNumber = "1748136441";
				u14.FirstName = "Jack";
				u14.LastName = "Mason";
				u14.Address = "444 45th St";
				u14.City = "Austin";
				u14.State = "TX";
				u14.Zip = "78701";
				u14.UserStatus = "Active";
				IdentityResult resultu14 = await _userManager.CreateAsync(u14,"rankmary");
				if(resultu14.Succeeded)
				{
					await _userManager.AddToRoleAsync(u14,"Employee");
				}
				AppUsers.Add(u14);

				AppUser u15 = new AppUser();
				u15.UserName = "c.miller@bevosbooks.com";
				u15.Email = "c.miller@bevosbooks.com";
				u15.PhoneNumber = "8999319585";
				u15.FirstName = "Charles";
				u15.LastName = "Miller";
				u15.Address = "8962 Main St.";
				u15.City = "Austin";
				u15.State = "TX";
				u15.Zip = "78709";
				u15.UserStatus = "Active";
				IdentityResult resultu15 = await _userManager.CreateAsync(u15,"kindly");
				if(resultu15.Succeeded)
				{
					await _userManager.AddToRoleAsync(u15,"Employee");
				}
				AppUsers.Add(u15);

				AppUser u16 = new AppUser();
				u16.UserName = "m.nguyen@bevosbooks.com";
				u16.Email = "m.nguyen@bevosbooks.com";
				u16.PhoneNumber = "8716746381";
				u16.FirstName = "Mary";
				u16.LastName = "Nguyen";
				u16.Address = "465 N. Bear Cub";
				u16.City = "Austin";
				u16.State = "TX";
				u16.Zip = "78734";
				u16.UserStatus = "Active";
				IdentityResult resultu16 = await _userManager.CreateAsync(u16,"ricearoni");
				if(resultu16.Succeeded)
				{
					await _userManager.AddToRoleAsync(u16,"Employee");
				}
				AppUsers.Add(u16);

				AppUser u17 = new AppUser();
				u17.UserName = "s.rankin@bevosbooks.com";
				u17.Email = "s.rankin@bevosbooks.com";
				u17.PhoneNumber = "5239029525";
				u17.FirstName = "Suzie";
				u17.LastName = "Rankin";
				u17.Address = "23 Dewey Road";
				u17.City = "Austin";
				u17.State = "TX";
				u17.Zip = "78712";
				u17.UserStatus = "Active";
				IdentityResult resultu17 = await _userManager.CreateAsync(u17,"walkamile");
				if(resultu17.Succeeded)
				{
					await _userManager.AddToRoleAsync(u17,"Employee");
				}
				AppUsers.Add(u17);

				AppUser u18 = new AppUser();
				u18.UserName = "m.rhodes@bevosbooks.com";
				u18.Email = "m.rhodes@bevosbooks.com";
				u18.PhoneNumber = "1232139514";
				u18.FirstName = "Megan";
				u18.LastName = "Rhodes";
				u18.Address = "4587 Enfield Rd.";
				u18.City = "Austin";
				u18.State = "TX";
				u18.Zip = "78729";
				u18.UserStatus = "Active";
				IdentityResult resultu18 = await _userManager.CreateAsync(u18,"ingram45");
				if(resultu18.Succeeded)
				{
					await _userManager.AddToRoleAsync(u18,"Employee");
				}
				AppUsers.Add(u18);

				AppUser u19 = new AppUser();
				u19.UserName = "e.rice@bevosbooks.com";
				u19.Email = "e.rice@bevosbooks.com";
				u19.PhoneNumber = "2706602803";
				u19.FirstName = "Eryn";
				u19.LastName = "Rice";
				u19.Address = "3405 Rio Grande";
				u19.City = "Austin";
				u19.State = "TX";
				u19.Zip = "78746";
				u19.UserStatus = "Active";
				IdentityResult resultu19 = await _userManager.CreateAsync(u19,"arched");
				if(resultu19.Succeeded)
				{
					await _userManager.AddToRoleAsync(u19,"Manager");
				}
				AppUsers.Add(u19);

				AppUser u20 = new AppUser();
				u20.UserName = "a.rogers@bevosbooks.com";
				u20.Email = "a.rogers@bevosbooks.com";
				u20.PhoneNumber = "4139645586";
				u20.FirstName = "Allen";
				u20.LastName = "Rogers";
				u20.Address = "4965 Oak Hill";
				u20.City = "Austin";
				u20.State = "TX";
				u20.Zip = "78705";
				u20.UserStatus = "Active";
				IdentityResult resultu20 = await _userManager.CreateAsync(u20,"lottery");
				if(resultu20.Succeeded)
				{
					await _userManager.AddToRoleAsync(u20,"Manager");
				}
				AppUsers.Add(u20);

				AppUser u21 = new AppUser();
				u21.UserName = "s.saunders@bevosbooks.com";
				u21.Email = "s.saunders@bevosbooks.com";
				u21.PhoneNumber = "9036349587";
				u21.FirstName = "Sarah";
				u21.LastName = "Saunders";
				u21.Address = "332 Avenue C";
				u21.City = "Austin";
				u21.State = "TX";
				u21.Zip = "78733";
				u21.UserStatus = "Active";
				IdentityResult resultu21 = await _userManager.CreateAsync(u21,"nostalgic");
				if(resultu21.Succeeded)
				{
					await _userManager.AddToRoleAsync(u21,"Employee");
				}
				AppUsers.Add(u21);

				AppUser u22 = new AppUser();
				u22.UserName = "w.sewell@bevosbooks.com";
				u22.Email = "w.sewell@bevosbooks.com";
				u22.PhoneNumber = "7224308314";
				u22.FirstName = "William";
				u22.LastName = "Sewell";
				u22.Address = "2365 51st St.";
				u22.City = "Austin";
				u22.State = "TX";
				u22.Zip = "78755";
				u22.UserStatus = "Active";
				IdentityResult resultu22 = await _userManager.CreateAsync(u22,"offbeat");
				if(resultu22.Succeeded)
				{
					await _userManager.AddToRoleAsync(u22,"Manager");
				}
				AppUsers.Add(u22);

				AppUser u23 = new AppUser();
				u23.UserName = "m.sheffield@bevosbooks.com";
				u23.Email = "m.sheffield@bevosbooks.com";
				u23.PhoneNumber = "9349192978";
				u23.FirstName = "Martin";
				u23.LastName = "Sheffield";
				u23.Address = "3886 Avenue A";
				u23.City = "San Marcos";
				u23.State = "TX";
				u23.Zip = "78666";
				u23.UserStatus = "Active";
				IdentityResult resultu23 = await _userManager.CreateAsync(u23,"evanescent");
				if(resultu23.Succeeded)
				{
					await _userManager.AddToRoleAsync(u23,"Employee");
				}
				AppUsers.Add(u23);

				AppUser u24 = new AppUser();
				u24.UserName = "c.silva@bevosbooks.com";
				u24.Email = "c.silva@bevosbooks.com";
				u24.PhoneNumber = "4874328170";
				u24.FirstName = "Cindy";
				u24.LastName = "Silva";
				u24.Address = "900 4th St";
				u24.City = "Austin";
				u24.State = "TX";
				u24.Zip = "78758";
				u24.UserStatus = "Active";
				IdentityResult resultu24 = await _userManager.CreateAsync(u24,"stewboy");
				if(resultu24.Succeeded)
				{
					await _userManager.AddToRoleAsync(u24,"Employee");
				}
				AppUsers.Add(u24);

				AppUser u25 = new AppUser();
				u25.UserName = "e.stuart@bevosbooks.com";
				u25.Email = "e.stuart@bevosbooks.com";
				u25.PhoneNumber = "1967846827";
				u25.FirstName = "Eric";
				u25.LastName = "Stuart";
				u25.Address = "5576 Toro Ring";
				u25.City = "Austin";
				u25.State = "TX";
				u25.Zip = "78758";
				u25.UserStatus = "Active";
				IdentityResult resultu25 = await _userManager.CreateAsync(u25,"instrument");
				if(resultu25.Succeeded)
				{
					await _userManager.AddToRoleAsync(u25,"Employee");
				}
				AppUsers.Add(u25);

				AppUser u26 = new AppUser();
				u26.UserName = "j.tanner@bevosbooks.com";
				u26.Email = "j.tanner@bevosbooks.com";
				u26.PhoneNumber = "5923026779";
				u26.FirstName = "Jeremy";
				u26.LastName = "Tanner";
				u26.Address = "4347 Almstead";
				u26.City = "Austin";
				u26.State = "TX";
				u26.Zip = "78712";
				u26.UserStatus = "Active";
				IdentityResult resultu26 = await _userManager.CreateAsync(u26,"hecktour");
				if(resultu26.Succeeded)
				{
					await _userManager.AddToRoleAsync(u26,"Employee");
				}
				AppUsers.Add(u26);

				AppUser u27 = new AppUser();
				u27.UserName = "a.taylor@bevosbooks.com";
				u27.Email = "a.taylor@bevosbooks.com";
				u27.PhoneNumber = "7246195827";
				u27.FirstName = "Allison";
				u27.LastName = "Taylor";
				u27.Address = "467 Nueces St.";
				u27.City = "Austin";
				u27.State = "TX";
				u27.Zip = "78727";
				u27.UserStatus = "Active";
				IdentityResult resultu27 = await _userManager.CreateAsync(u27,"countryrhodes");
				if(resultu27.Succeeded)
				{
					await _userManager.AddToRoleAsync(u27,"Employee");
				}
				AppUsers.Add(u27);

				AppUser u28 = new AppUser();
				u28.UserName = "r.taylor@bevosbooks.com";
				u28.Email = "r.taylor@bevosbooks.com";
				u28.PhoneNumber = "9071236087";
				u28.FirstName = "Rachel";
				u28.LastName = "Taylor";
				u28.Address = "345 Longview Dr.";
				u28.City = "Austin";
				u28.State = "TX";
				u28.Zip = "78746";
				u28.UserStatus = "Active";
				IdentityResult resultu28 = await _userManager.CreateAsync(u28,"landus");
				if(resultu28.Succeeded)
				{
					await _userManager.AddToRoleAsync(u28,"Manager");
				}
				AppUsers.Add(u28);

				//loop through employees
				foreach (AppUser ur in AppUsers)
				{
					//set name of User to help debug
					employeeEmail = ur.Email;

					//see if repo exists in database
                    AppUser dbur = db.Users.FirstOrDefault(r => r.Email == ur.Email);

					if (dbur == null) //user does not exist in database
					{
                        db.Users.Add(ur);
						db.SaveChanges();
						intEmployeesAdded += 1;
					}
					else
					{
						dbur.UserName = ur.UserName;
						dbur.Email = ur.Email;
						dbur.PhoneNumber = ur.PhoneNumber;
						dbur.FirstName = ur.FirstName;
						dbur.LastName = ur.LastName;
						dbur.Address = ur.Address;
						dbur.City = ur.City;
						dbur.State = ur.State;
						dbur.Zip = ur.Zip;
						db.Update(dbur);
						db.SaveChanges();
					}
				}
			}
			catch
			{
				String msg = "Users added:" + intEmployeesAdded + "; Error on " + employeeEmail;
				throw new InvalidOperationException(msg);
			}
		}
	}
}

using fa18Team22.Models;
using fa18Team22.DAL;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace fa18Team22.Seeding
{
	public static class SeedCustomers
	{
		public static async System.Threading.Tasks.Task SeedAllCustomersAsync(AppDbContext db, IServiceProvider serviceProvider)
		{
			UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			if (db.Users.Count() == 51)
			{
				throw new NotSupportedException("The database already contains all 51 customers!");
			}

			Int32 intCustomersAdded = 0;
			String customerEmail = "Begin"; //helps to keep track of error on repos
			List<AppUser> AppUsers = new List<AppUser>();

			try
			{
				AppUser u1 = new AppUser();
				u1.UserName = "cbaker@example.com";
				u1.Email = "cbaker@example.com";
				u1.PhoneNumber = "5725458641";
				u1.FirstName = "Christopher";
				u1.LastName = "Baker";
				u1.Address = "1898 Schurz Alley";
				u1.City = "Austin";
				u1.State = "TX";
				u1.Zip = "78705";
				u1.UserStatus = "Active";
				IdentityResult resultu1 = await _userManager.CreateAsync(u1,"bookworm");
				if(resultu1.Succeeded)
				{
					await _userManager.AddToRoleAsync(u1,"Customer");
				}
				AppUsers.Add(u1);

				AppUser u2 = new AppUser();
				u2.UserName = "banker@longhorn.net";
				u2.Email = "banker@longhorn.net";
				u2.PhoneNumber = "9867048435";
				u2.FirstName = "Michelle";
				u2.LastName = "Banks";
				u2.Address = "97 Elmside Pass";
				u2.City = "Austin";
				u2.State = "TX";
				u2.Zip = "78712";
				u2.UserStatus = "Active";
				IdentityResult resultu2 = await _userManager.CreateAsync(u2,"potato");
				if(resultu2.Succeeded)
				{
					await _userManager.AddToRoleAsync(u2,"Customer");
				}
				AppUsers.Add(u2);

				AppUser u3 = new AppUser();
				u3.UserName = "franco@example.com";
				u3.Email = "franco@example.com";
				u3.PhoneNumber = "6836109514";
				u3.FirstName = "Franco";
				u3.LastName = "Broccolo";
				u3.Address = "88 Crowley Circle";
				u3.City = "Austin";
				u3.State = "TX";
				u3.Zip = "78786";
				u3.UserStatus = "Active";
				IdentityResult resultu3 = await _userManager.CreateAsync(u3,"painting");
				if(resultu3.Succeeded)
				{
					await _userManager.AddToRoleAsync(u3,"Customer");
				}
				AppUsers.Add(u3);

				AppUser u4 = new AppUser();
				u4.UserName = "wchang@example.com";
				u4.Email = "wchang@example.com";
				u4.PhoneNumber = "7070911071";
				u4.FirstName = "Wendy";
				u4.LastName = "Chang";
				u4.Address = "56560 Sage Junction";
				u4.City = "Eagle Pass";
				u4.State = "TX";
				u4.Zip = "78852";
				u4.UserStatus = "Active";
				IdentityResult resultu4 = await _userManager.CreateAsync(u4,"texas1");
				if(resultu4.Succeeded)
				{
					await _userManager.AddToRoleAsync(u4,"Customer");
				}
				AppUsers.Add(u4);

				AppUser u5 = new AppUser();
				u5.UserName = "limchou@gogle.com";
				u5.Email = "limchou@gogle.com";
				u5.PhoneNumber = "1488907687";
				u5.FirstName = "Lim";
				u5.LastName = "Chou";
				u5.Address = "60 Lunder Point";
				u5.City = "Austin";
				u5.State = "TX";
				u5.Zip = "78729";
				u5.UserStatus = "Active";
				IdentityResult resultu5 = await _userManager.CreateAsync(u5,"Anchorage");
				if(resultu5.Succeeded)
				{
					await _userManager.AddToRoleAsync(u5,"Customer");
				}
				AppUsers.Add(u5);

				AppUser u6 = new AppUser();
				u6.UserName = "shdixon@aoll.com";
				u6.Email = "shdixon@aoll.com";
				u6.PhoneNumber = "6899701824";
				u6.FirstName = "Shan";
				u6.LastName = "Dixon";
				u6.Address = "9448 Pleasure Avenue";
				u6.City = "Georgetown";
				u6.State = "TX";
				u6.Zip = "78628";
				u6.UserStatus = "Active";
				IdentityResult resultu6 = await _userManager.CreateAsync(u6,"aggies");
				if(resultu6.Succeeded)
				{
					await _userManager.AddToRoleAsync(u6,"Customer");
				}
				AppUsers.Add(u6);

				AppUser u7 = new AppUser();
				u7.UserName = "j.b.evans@aheca.org";
				u7.Email = "j.b.evans@aheca.org";
				u7.PhoneNumber = "9986825917";
				u7.FirstName = "Jim Bob";
				u7.LastName = "Evans";
				u7.Address = "51 Emmet Parkway";
				u7.City = "Austin";
				u7.State = "TX";
				u7.Zip = "78705";
				u7.UserStatus = "Active";
				IdentityResult resultu7 = await _userManager.CreateAsync(u7,"hampton1");
				if(resultu7.Succeeded)
				{
					await _userManager.AddToRoleAsync(u7,"Customer");
				}
				AppUsers.Add(u7);

				AppUser u8 = new AppUser();
				u8.UserName = "feeley@penguin.org";
				u8.Email = "feeley@penguin.org";
				u8.PhoneNumber = "3464121966";
				u8.FirstName = "Lou Ann";
				u8.LastName = "Feeley";
				u8.Address = "65 Darwin Crossing";
				u8.City = "Austin";
				u8.State = "TX";
				u8.Zip = "78704";
				u8.UserStatus = "Active";
				IdentityResult resultu8 = await _userManager.CreateAsync(u8,"longhorns");
				if(resultu8.Succeeded)
				{
					await _userManager.AddToRoleAsync(u8,"Customer");
				}
				AppUsers.Add(u8);

				AppUser u9 = new AppUser();
				u9.UserName = "tfreeley@minnetonka.ci.us";
				u9.Email = "tfreeley@minnetonka.ci.us";
				u9.PhoneNumber = "6581357270";
				u9.FirstName = "Tesa";
				u9.LastName = "Freeley";
				u9.Address = "7352 Loftsgordon Court";
				u9.City = "College Station";
				u9.State = "TX";
				u9.Zip = "77840";
				u9.UserStatus = "Active";
				IdentityResult resultu9 = await _userManager.CreateAsync(u9,"mustangs");
				if(resultu9.Succeeded)
				{
					await _userManager.AddToRoleAsync(u9,"Customer");
				}
				AppUsers.Add(u9);

				AppUser u10 = new AppUser();
				u10.UserName = "mgarcia@gogle.com";
				u10.Email = "mgarcia@gogle.com";
				u10.PhoneNumber = "3767347949";
				u10.FirstName = "Margaret";
				u10.LastName = "Garcia";
				u10.Address = "7 International Road";
				u10.City = "Austin";
				u10.State = "TX";
				u10.Zip = "78756";
				u10.UserStatus = "Active";
				IdentityResult resultu10 = await _userManager.CreateAsync(u10,"onetime");
				if(resultu10.Succeeded)
				{
					await _userManager.AddToRoleAsync(u10,"Customer");
				}
				AppUsers.Add(u10);

				AppUser u11 = new AppUser();
				u11.UserName = "chaley@thug.com";
				u11.Email = "chaley@thug.com";
				u11.PhoneNumber = "2198604221";
				u11.FirstName = "Charles";
				u11.LastName = "Haley";
				u11.Address = "8 Warrior Trail";
				u11.City = "Austin";
				u11.State = "TX";
				u11.Zip = "78746";
				u11.UserStatus = "Active";
				IdentityResult resultu11 = await _userManager.CreateAsync(u11,"pepperoni");
				if(resultu11.Succeeded)
				{
					await _userManager.AddToRoleAsync(u11,"Customer");
				}
				AppUsers.Add(u11);

				AppUser u12 = new AppUser();
				u12.UserName = "jeffh@sonic.com";
				u12.Email = "jeffh@sonic.com";
				u12.PhoneNumber = "1222185888";
				u12.FirstName = "Jeffrey";
				u12.LastName = "Hampton";
				u12.Address = "9107 Lighthouse Bay Road";
				u12.City = "Austin";
				u12.State = "TX";
				u12.Zip = "78756";
				u12.UserStatus = "Active";
				IdentityResult resultu12 = await _userManager.CreateAsync(u12,"raiders");
				if(resultu12.Succeeded)
				{
					await _userManager.AddToRoleAsync(u12,"Customer");
				}
				AppUsers.Add(u12);

				AppUser u13 = new AppUser();
				u13.UserName = "wjhearniii@umich.org";
				u13.Email = "wjhearniii@umich.org";
				u13.PhoneNumber = "5123071976";
				u13.FirstName = "John";
				u13.LastName = "Hearn";
				u13.Address = "59784 Pierstorff Center";
				u13.City = "Liberty";
				u13.State = "TX";
				u13.Zip = "77575";
				u13.UserStatus = "Active";
				IdentityResult resultu13 = await _userManager.CreateAsync(u13,"jhearn22");
				if(resultu13.Succeeded)
				{
					await _userManager.AddToRoleAsync(u13,"Customer");
				}
				AppUsers.Add(u13);

				AppUser u14 = new AppUser();
				u14.UserName = "ahick@yaho.com";
				u14.Email = "ahick@yaho.com";
				u14.PhoneNumber = "1211949601";
				u14.FirstName = "Anthony";
				u14.LastName = "Hicks";
				u14.Address = "932 Monica Way";
				u14.City = "San Antonio";
				u14.State = "TX";
				u14.Zip = "78203";
				u14.UserStatus = "Active";
				IdentityResult resultu14 = await _userManager.CreateAsync(u14,"hickhickup");
				if(resultu14.Succeeded)
				{
					await _userManager.AddToRoleAsync(u14,"Customer");
				}
				AppUsers.Add(u14);

				AppUser u15 = new AppUser();
				u15.UserName = "ingram@jack.com";
				u15.Email = "ingram@jack.com";
				u15.PhoneNumber = "1372121569";
				u15.FirstName = "Brad";
				u15.LastName = "Ingram";
				u15.Address = "4 Lukken Court";
				u15.City = "New Braunfels";
				u15.State = "TX";
				u15.Zip = "78132";
				u15.UserStatus = "Active";
				IdentityResult resultu15 = await _userManager.CreateAsync(u15,"ingram2015");
				if(resultu15.Succeeded)
				{
					await _userManager.AddToRoleAsync(u15,"Customer");
				}
				AppUsers.Add(u15);

				AppUser u16 = new AppUser();
				u16.UserName = "toddj@yourmom.com";
				u16.Email = "toddj@yourmom.com";
				u16.PhoneNumber = "8543163836";
				u16.FirstName = "Todd";
				u16.LastName = "Jacobs";
				u16.Address = "7 Susan Junction";
				u16.City = "New York";
				u16.State = "NY";
				u16.Zip = "10101";
				u16.UserStatus = "Active";
				IdentityResult resultu16 = await _userManager.CreateAsync(u16,"toddy25");
				if(resultu16.Succeeded)
				{
					await _userManager.AddToRoleAsync(u16,"Customer");
				}
				AppUsers.Add(u16);

				AppUser u17 = new AppUser();
				u17.UserName = "thequeen@aska.net";
				u17.Email = "thequeen@aska.net";
				u17.PhoneNumber = "3214163359";
				u17.FirstName = "Victoria";
				u17.LastName = "Lawrence";
				u17.Address = "669 Oak Junction";
				u17.City = "Lockhart";
				u17.State = "TX";
				u17.Zip = "78644";
				u17.UserStatus = "Active";
				IdentityResult resultu17 = await _userManager.CreateAsync(u17,"something");
				if(resultu17.Succeeded)
				{
					await _userManager.AddToRoleAsync(u17,"Customer");
				}
				AppUsers.Add(u17);

				AppUser u18 = new AppUser();
				u18.UserName = "linebacker@gogle.com";
				u18.Email = "linebacker@gogle.com";
				u18.PhoneNumber = "2505265350";
				u18.FirstName = "Erik";
				u18.LastName = "Lineback";
				u18.Address = "099 Luster Point";
				u18.City = "Kingwood";
				u18.State = "TX";
				u18.Zip = "77325";
				u18.UserStatus = "Active";
				IdentityResult resultu18 = await _userManager.CreateAsync(u18,"Password1");
				if(resultu18.Succeeded)
				{
					await _userManager.AddToRoleAsync(u18,"Customer");
				}
				AppUsers.Add(u18);

				AppUser u19 = new AppUser();
				u19.UserName = "elowe@netscare.net";
				u19.Email = "elowe@netscare.net";
				u19.PhoneNumber = "4070619503";
				u19.FirstName = "Ernest";
				u19.LastName = "Lowe";
				u19.Address = "35473 Hansons Hill";
				u19.City = "Beverly Hills";
				u19.State = "CA";
				u19.Zip = "90210";
				u19.UserStatus = "Active";
				IdentityResult resultu19 = await _userManager.CreateAsync(u19,"aclfest2017");
				if(resultu19.Succeeded)
				{
					await _userManager.AddToRoleAsync(u19,"Customer");
				}
				AppUsers.Add(u19);

				AppUser u20 = new AppUser();
				u20.UserName = "cluce@gogle.com";
				u20.Email = "cluce@gogle.com";
				u20.PhoneNumber = "7358436110";
				u20.FirstName = "Chuck";
				u20.LastName = "Luce";
				u20.Address = "4 Emmet Junction";
				u20.City = "Navasota";
				u20.State = "TX";
				u20.Zip = "77868";
				u20.UserStatus = "Active";
				IdentityResult resultu20 = await _userManager.CreateAsync(u20,"nothinggood");
				if(resultu20.Succeeded)
				{
					await _userManager.AddToRoleAsync(u20,"Customer");
				}
				AppUsers.Add(u20);

				AppUser u21 = new AppUser();
				u21.UserName = "mackcloud@george.com";
				u21.Email = "mackcloud@george.com";
				u21.PhoneNumber = "7240178229";
				u21.FirstName = "Jennifer";
				u21.LastName = "MacLeod";
				u21.Address = "3 Orin Road";
				u21.City = "Austin";
				u21.State = "TX";
				u21.Zip = "78712";
				u21.UserStatus = "Active";
				IdentityResult resultu21 = await _userManager.CreateAsync(u21,"whatever");
				if(resultu21.Succeeded)
				{
					await _userManager.AddToRoleAsync(u21,"Customer");
				}
				AppUsers.Add(u21);

				AppUser u22 = new AppUser();
				u22.UserName = "cmartin@beets.com";
				u22.Email = "cmartin@beets.com";
				u22.PhoneNumber = "2495200223";
				u22.FirstName = "Elizabeth";
				u22.LastName = "Markham";
				u22.Address = "8171 Commercial Crossing";
				u22.City = "Austin";
				u22.State = "TX";
				u22.Zip = "78712";
				u22.UserStatus = "Active";
				IdentityResult resultu22 = await _userManager.CreateAsync(u22,"snowsnow");
				if(resultu22.Succeeded)
				{
					await _userManager.AddToRoleAsync(u22,"Customer");
				}
				AppUsers.Add(u22);

				AppUser u23 = new AppUser();
				u23.UserName = "clarence@yoho.com";
				u23.Email = "clarence@yoho.com";
				u23.PhoneNumber = "4086179161";
				u23.FirstName = "Clarence";
				u23.LastName = "Martin";
				u23.Address = "96 Anthes Place";
				u23.City = "Schenectady";
				u23.State = "NY";
				u23.Zip = "12345";
				u23.UserStatus = "Active";
				IdentityResult resultu23 = await _userManager.CreateAsync(u23,"whocares");
				if(resultu23.Succeeded)
				{
					await _userManager.AddToRoleAsync(u23,"Customer");
				}
				AppUsers.Add(u23);

				AppUser u24 = new AppUser();
				u24.UserName = "gregmartinez@drdre.com";
				u24.Email = "gregmartinez@drdre.com";
				u24.PhoneNumber = "9371927523";
				u24.FirstName = "Gregory";
				u24.LastName = "Martinez";
				u24.Address = "10 Northridge Plaza";
				u24.City = "Austin";
				u24.State = "TX";
				u24.Zip = "78717";
				u24.UserStatus = "Active";
				IdentityResult resultu24 = await _userManager.CreateAsync(u24,"xcellent");
				if(resultu24.Succeeded)
				{
					await _userManager.AddToRoleAsync(u24,"Customer");
				}
				AppUsers.Add(u24);

				AppUser u25 = new AppUser();
				u25.UserName = "cmiller@bob.com";
				u25.Email = "cmiller@bob.com";
				u25.PhoneNumber = "5954063857";
				u25.FirstName = "Charles";
				u25.LastName = "Miller";
				u25.Address = "87683 Schmedeman Circle";
				u25.City = "Austin";
				u25.State = "TX";
				u25.Zip = "78727";
				u25.UserStatus = "Active";
				IdentityResult resultu25 = await _userManager.CreateAsync(u25,"mydogspot");
				if(resultu25.Succeeded)
				{
					await _userManager.AddToRoleAsync(u25,"Customer");
				}
				AppUsers.Add(u25);

				AppUser u26 = new AppUser();
				u26.UserName = "knelson@aoll.com";
				u26.Email = "knelson@aoll.com";
				u26.PhoneNumber = "8929209512";
				u26.FirstName = "Kelly";
				u26.LastName = "Nelson";
				u26.Address = "3244 Ludington Court";
				u26.City = "Beaumont";
				u26.State = "TX";
				u26.Zip = "77720";
				u26.UserStatus = "Active";
				IdentityResult resultu26 = await _userManager.CreateAsync(u26,"spotmydog");
				if(resultu26.Succeeded)
				{
					await _userManager.AddToRoleAsync(u26,"Customer");
				}
				AppUsers.Add(u26);

				AppUser u27 = new AppUser();
				u27.UserName = "joewin@xfactor.com";
				u27.Email = "joewin@xfactor.com";
				u27.PhoneNumber = "9226301774";
				u27.FirstName = "Joe";
				u27.LastName = "Nguyen";
				u27.Address = "4780 Talisman Court";
				u27.City = "San Marcos";
				u27.State = "TX";
				u27.Zip = "78667";
				u27.UserStatus = "Active";
				IdentityResult resultu27 = await _userManager.CreateAsync(u27,"joejoejoe");
				if(resultu27.Succeeded)
				{
					await _userManager.AddToRoleAsync(u27,"Customer");
				}
				AppUsers.Add(u27);

				AppUser u28 = new AppUser();
				u28.UserName = "orielly@foxnews.cnn";
				u28.Email = "orielly@foxnews.cnn";
				u28.PhoneNumber = "2537646912";
				u28.FirstName = "Bill";
				u28.LastName = "O'Reilly";
				u28.Address = "4154 Delladonna Plaza";
				u28.City = "Bergheim";
				u28.State = "TX";
				u28.Zip = "78004";
				u28.UserStatus = "Active";
				IdentityResult resultu28 = await _userManager.CreateAsync(u28,"billyboy");
				if(resultu28.Succeeded)
				{
					await _userManager.AddToRoleAsync(u28,"Customer");
				}
				AppUsers.Add(u28);

				AppUser u29 = new AppUser();
				u29.UserName = "ankaisrad@gogle.com";
				u29.Email = "ankaisrad@gogle.com";
				u29.PhoneNumber = "2182889379";
				u29.FirstName = "Anka";
				u29.LastName = "Radkovich";
				u29.Address = "72361 Bayside Drive";
				u29.City = "Austin";
				u29.State = "TX";
				u29.Zip = "78789";
				u29.UserStatus = "Active";
				IdentityResult resultu29 = await _userManager.CreateAsync(u29,"radgirl");
				if(resultu29.Succeeded)
				{
					await _userManager.AddToRoleAsync(u29,"Customer");
				}
				AppUsers.Add(u29);

				AppUser u30 = new AppUser();
				u30.UserName = "megrhodes@freserve.co.uk";
				u30.Email = "megrhodes@freserve.co.uk";
				u30.PhoneNumber = "9532396075";
				u30.FirstName = "Megan";
				u30.LastName = "Rhodes";
				u30.Address = "76875 Hoffman Point";
				u30.City = "Orlando";
				u30.State = "FL";
				u30.Zip = "32830";
				u30.UserStatus = "Active";
				IdentityResult resultu30 = await _userManager.CreateAsync(u30,"meganr34");
				if(resultu30.Succeeded)
				{
					await _userManager.AddToRoleAsync(u30,"Customer");
				}
				AppUsers.Add(u30);

				AppUser u31 = new AppUser();
				u31.UserName = "erynrice@aoll.com";
				u31.Email = "erynrice@aoll.com";
				u31.PhoneNumber = "7303815953";
				u31.FirstName = "Eryn";
				u31.LastName = "Rice";
				u31.Address = "048 Elmside Park";
				u31.City = "South Padre Island";
				u31.State = "TX";
				u31.Zip = "78597";
				u31.UserStatus = "Active";
				IdentityResult resultu31 = await _userManager.CreateAsync(u31,"ricearoni");
				if(resultu31.Succeeded)
				{
					await _userManager.AddToRoleAsync(u31,"Customer");
				}
				AppUsers.Add(u31);

				AppUser u32 = new AppUser();
				u32.UserName = "jorge@noclue.com";
				u32.Email = "jorge@noclue.com";
				u32.PhoneNumber = "3677322422";
				u32.FirstName = "Jorge";
				u32.LastName = "Rodriguez";
				u32.Address = "01 Browning Pass";
				u32.City = "Austin";
				u32.State = "TX";
				u32.Zip = "78744";
				u32.UserStatus = "Active";
				IdentityResult resultu32 = await _userManager.CreateAsync(u32,"alaskaboy");
				if(resultu32.Succeeded)
				{
					await _userManager.AddToRoleAsync(u32,"Customer");
				}
				AppUsers.Add(u32);

				AppUser u33 = new AppUser();
				u33.UserName = "mrrogers@lovelyday.com";
				u33.Email = "mrrogers@lovelyday.com";
				u33.PhoneNumber = "3911705385";
				u33.FirstName = "Allen";
				u33.LastName = "Rogers";
				u33.Address = "844 Anderson Alley";
				u33.City = "Canyon Lake";
				u33.State = "TX";
				u33.Zip = "78133";
				u33.UserStatus = "Active";
				IdentityResult resultu33 = await _userManager.CreateAsync(u33,"bunnyhop");
				if(resultu33.Succeeded)
				{
					await _userManager.AddToRoleAsync(u33,"Customer");
				}
				AppUsers.Add(u33);

				AppUser u34 = new AppUser();
				u34.UserName = "stjean@athome.com";
				u34.Email = "stjean@athome.com";
				u34.PhoneNumber = "7351610920";
				u34.FirstName = "Olivier";
				u34.LastName = "Saint-Jean";
				u34.Address = "1891 Docker Point";
				u34.City = "Austin";
				u34.State = "TX";
				u34.Zip = "78779";
				u34.UserStatus = "Active";
				IdentityResult resultu34 = await _userManager.CreateAsync(u34,"dustydusty");
				if(resultu34.Succeeded)
				{
					await _userManager.AddToRoleAsync(u34,"Customer");
				}
				AppUsers.Add(u34);

				AppUser u35 = new AppUser();
				u35.UserName = "saunders@pen.com";
				u35.Email = "saunders@pen.com";
				u35.PhoneNumber = "5269661692";
				u35.FirstName = "Sarah";
				u35.LastName = "Saunders";
				u35.Address = "1469 Upham Road";
				u35.City = "Austin";
				u35.State = "TX";
				u35.Zip = "78720";
				u35.UserStatus = "Active";
				IdentityResult resultu35 = await _userManager.CreateAsync(u35,"jrod2017");
				if(resultu35.Succeeded)
				{
					await _userManager.AddToRoleAsync(u35,"Customer");
				}
				AppUsers.Add(u35);

				AppUser u36 = new AppUser();
				u36.UserName = "willsheff@email.com";
				u36.Email = "willsheff@email.com";
				u36.PhoneNumber = "1875727246";
				u36.FirstName = "William";
				u36.LastName = "Sewell";
				u36.Address = "1672 Oak Valley Circle";
				u36.City = "Austin";
				u36.State = "TX";
				u36.Zip = "78705";
				u36.UserStatus = "Active";
				IdentityResult resultu36 = await _userManager.CreateAsync(u36,"martin1234");
				if(resultu36.Succeeded)
				{
					await _userManager.AddToRoleAsync(u36,"Customer");
				}
				AppUsers.Add(u36);

				AppUser u37 = new AppUser();
				u37.UserName = "sheffiled@gogle.com";
				u37.Email = "sheffiled@gogle.com";
				u37.PhoneNumber = "1394323615";
				u37.FirstName = "Martin";
				u37.LastName = "Sheffield";
				u37.Address = "816 Kennedy Place";
				u37.City = "Round Rock";
				u37.State = "TX";
				u37.Zip = "78680";
				u37.UserStatus = "Active";
				IdentityResult resultu37 = await _userManager.CreateAsync(u37,"penguin12");
				if(resultu37.Succeeded)
				{
					await _userManager.AddToRoleAsync(u37,"Customer");
				}
				AppUsers.Add(u37);

				AppUser u38 = new AppUser();
				u38.UserName = "johnsmith187@aoll.com";
				u38.Email = "johnsmith187@aoll.com";
				u38.PhoneNumber = "6645937874";
				u38.FirstName = "John";
				u38.LastName = "Smith";
				u38.Address = "0745 Golf Road";
				u38.City = "Austin";
				u38.State = "TX";
				u38.Zip = "78760";
				u38.UserStatus = "Active";
				IdentityResult resultu38 = await _userManager.CreateAsync(u38,"rogerthat");
				if(resultu38.Succeeded)
				{
					await _userManager.AddToRoleAsync(u38,"Customer");
				}
				AppUsers.Add(u38);

				AppUser u39 = new AppUser();
				u39.UserName = "dustroud@mail.com";
				u39.Email = "dustroud@mail.com";
				u39.PhoneNumber = "6470254680";
				u39.FirstName = "Dustin";
				u39.LastName = "Stroud";
				u39.Address = "505 Dexter Plaza";
				u39.City = "Sweet Home";
				u39.State = "TX";
				u39.Zip = "77987";
				u39.UserStatus = "Active";
				IdentityResult resultu39 = await _userManager.CreateAsync(u39,"smitty444");
				if(resultu39.Succeeded)
				{
					await _userManager.AddToRoleAsync(u39,"Customer");
				}
				AppUsers.Add(u39);

				AppUser u40 = new AppUser();
				u40.UserName = "estuart@anchor.net";
				u40.Email = "estuart@anchor.net";
				u40.PhoneNumber = "7701621022";
				u40.FirstName = "Eric";
				u40.LastName = "Stuart";
				u40.Address = "585 Claremont Drive";
				u40.City = "Corpus Christi";
				u40.State = "TX";
				u40.Zip = "78412";
				u40.UserStatus = "Active";
				IdentityResult resultu40 = await _userManager.CreateAsync(u40,"stewball");
				if(resultu40.Succeeded)
				{
					await _userManager.AddToRoleAsync(u40,"Customer");
				}
				AppUsers.Add(u40);

				AppUser u41 = new AppUser();
				u41.UserName = "peterstump@noclue.com";
				u41.Email = "peterstump@noclue.com";
				u41.PhoneNumber = "2181960061";
				u41.FirstName = "Peter";
				u41.LastName = "Stump";
				u41.Address = "89035 Welch Circle";
				u41.City = "Pflugerville";
				u41.State = "TX";
				u41.Zip = "78660";
				u41.UserStatus = "Active";
				IdentityResult resultu41 = await _userManager.CreateAsync(u41,"slowwind");
				if(resultu41.Succeeded)
				{
					await _userManager.AddToRoleAsync(u41,"Customer");
				}
				AppUsers.Add(u41);

				AppUser u42 = new AppUser();
				u42.UserName = "jtanner@mustang.net";
				u42.Email = "jtanner@mustang.net";
				u42.PhoneNumber = "9908469499";
				u42.FirstName = "Jeremy";
				u42.LastName = "Tanner";
				u42.Address = "4 Stang Trail";
				u42.City = "Austin";
				u42.State = "TX";
				u42.Zip = "78702";
				u42.UserStatus = "Active";
				IdentityResult resultu42 = await _userManager.CreateAsync(u42,"tanner5454");
				if(resultu42.Succeeded)
				{
					await _userManager.AddToRoleAsync(u42,"Customer");
				}
				AppUsers.Add(u42);

				AppUser u43 = new AppUser();
				u43.UserName = "taylordjay@aoll.com";
				u43.Email = "taylordjay@aoll.com";
				u43.PhoneNumber = "7011918647";
				u43.FirstName = "Allison";
				u43.LastName = "Taylor";
				u43.Address = "726 Twin Pines Avenue";
				u43.City = "Austin";
				u43.State = "TX";
				u43.Zip = "78713";
				u43.UserStatus = "Active";
				IdentityResult resultu43 = await _userManager.CreateAsync(u43,"allyrally");
				if(resultu43.Succeeded)
				{
					await _userManager.AddToRoleAsync(u43,"Customer");
				}
				AppUsers.Add(u43);

				AppUser u44 = new AppUser();
				u44.UserName = "rtaylor@gogle.com";
				u44.Email = "rtaylor@gogle.com";
				u44.PhoneNumber = "8937910053";
				u44.FirstName = "Rachel";
				u44.LastName = "Taylor";
				u44.Address = "06605 Sugar Drive";
				u44.City = "Austin";
				u44.State = "TX";
				u44.Zip = "78712";
				u44.UserStatus = "Active";
				IdentityResult resultu44 = await _userManager.CreateAsync(u44,"taylorbaylor");
				if(resultu44.Succeeded)
				{
					await _userManager.AddToRoleAsync(u44,"Customer");
				}
				AppUsers.Add(u44);

				AppUser u45 = new AppUser();
				u45.UserName = "teefrank@noclue.com";
				u45.Email = "teefrank@noclue.com";
				u45.PhoneNumber = "6394568913";
				u45.FirstName = "Frank";
				u45.LastName = "Tee";
				u45.Address = "3567 Dawn Plaza";
				u45.City = "Austin";
				u45.State = "TX";
				u45.Zip = "78786";
				u45.UserStatus = "Active";
				IdentityResult resultu45 = await _userManager.CreateAsync(u45,"teeoff22");
				if(resultu45.Succeeded)
				{
					await _userManager.AddToRoleAsync(u45,"Customer");
				}
				AppUsers.Add(u45);

				AppUser u46 = new AppUser();
				u46.UserName = "ctucker@alphabet.co.uk";
				u46.Email = "ctucker@alphabet.co.uk";
				u46.PhoneNumber = "2676838676";
				u46.FirstName = "Clent";
				u46.LastName = "Tucker";
				u46.Address = "704 Northland Alley";
				u46.City = "San Antonio";
				u46.State = "TX";
				u46.Zip = "78279";
				u46.UserStatus = "Active";
				IdentityResult resultu46 = await _userManager.CreateAsync(u46,"tucksack1");
				if(resultu46.Succeeded)
				{
					await _userManager.AddToRoleAsync(u46,"Customer");
				}
				AppUsers.Add(u46);

				AppUser u47 = new AppUser();
				u47.UserName = "avelasco@yoho.com";
				u47.Email = "avelasco@yoho.com";
				u47.PhoneNumber = "3452909754";
				u47.FirstName = "Allen";
				u47.LastName = "Velasco";
				u47.Address = "72 Harbort Point";
				u47.City = "Navasota";
				u47.State = "TX";
				u47.Zip = "77868";
				u47.UserStatus = "Active";
				IdentityResult resultu47 = await _userManager.CreateAsync(u47,"meow88");
				if(resultu47.Succeeded)
				{
					await _userManager.AddToRoleAsync(u47,"Customer");
				}
				AppUsers.Add(u47);

				AppUser u48 = new AppUser();
				u48.UserName = "vinovino@grapes.com";
				u48.Email = "vinovino@grapes.com";
				u48.PhoneNumber = "8567089194";
				u48.FirstName = "Janet";
				u48.LastName = "Vino";
				u48.Address = "1 Oak Valley Place";
				u48.City = "Boston";
				u48.State = "MA";
				u48.Zip = "02114";
				u48.UserStatus = "Active";
				IdentityResult resultu48 = await _userManager.CreateAsync(u48,"vinovino");
				if(resultu48.Succeeded)
				{
					await _userManager.AddToRoleAsync(u48,"Customer");
				}
				AppUsers.Add(u48);

				AppUser u49 = new AppUser();
				u49.UserName = "westj@pioneer.net";
				u49.Email = "westj@pioneer.net";
				u49.PhoneNumber = "6260784394";
				u49.FirstName = "Jake";
				u49.LastName = "West";
				u49.Address = "48743 Banding Parkway";
				u49.City = "Marble Falls";
				u49.State = "TX";
				u49.Zip = "78654";
				u49.UserStatus = "Active";
				IdentityResult resultu49 = await _userManager.CreateAsync(u49,"gowest");
				if(resultu49.Succeeded)
				{
					await _userManager.AddToRoleAsync(u49,"Customer");
				}
				AppUsers.Add(u49);

				AppUser u50 = new AppUser();
				u50.UserName = "winner@hootmail.com";
				u50.Email = "winner@hootmail.com";
				u50.PhoneNumber = "3733971174";
				u50.FirstName = "Louis";
				u50.LastName = "Winthorpe";
				u50.Address = "96850 Summit Crossing";
				u50.City = "Austin";
				u50.State = "TX";
				u50.Zip = "78730";
				u50.UserStatus = "Active";
				IdentityResult resultu50 = await _userManager.CreateAsync(u50,"louielouie");
				if(resultu50.Succeeded)
				{
					await _userManager.AddToRoleAsync(u50,"Customer");
				}
				AppUsers.Add(u50);

				AppUser u51 = new AppUser();
				u51.UserName = "rwood@voyager.net";
				u51.Email = "rwood@voyager.net";
				u51.PhoneNumber = "8433359800";
				u51.FirstName = "Reagan";
				u51.LastName = "Wood";
				u51.Address = "18354 Bluejay Street";
				u51.City = "Austin";
				u51.State = "TX";
				u51.Zip = "78712";
				u51.UserStatus = "Active";
				IdentityResult resultu51 = await _userManager.CreateAsync(u51,"woodyman1");
				if(resultu51.Succeeded)
				{
					await _userManager.AddToRoleAsync(u51,"Customer");
				}
				AppUsers.Add(u51);

				//loop through customers
				foreach (AppUser ur in AppUsers)
				{
					//set name of User to help debug
					customerEmail = ur.Email;

					//see if repo exists in database
					AppUser dbur = db.Users.FirstOrDefault(r => r.Email == ur.Email);

					if (dbur == null) //user does not exist in database
					{
						db.Users.Add(ur);
						db.SaveChanges();
						intCustomersAdded += 1;
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
				String msg = "Users added:" + intCustomersAdded + "; Error on " + customerEmail;
				throw new InvalidOperationException(msg);
			}
		}
	}
}

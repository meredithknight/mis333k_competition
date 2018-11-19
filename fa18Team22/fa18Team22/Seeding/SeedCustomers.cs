using fa18Team22.Models;
using fa18Team22.DAL;
using System.Collections.Generic;
using System;
using System.Linq;

namespace fa18Team22.Seeding
{
	public static class SeedCustomers
	{
		public static void SeedAllCustomers(AppDbContext db)
		{
			if (db.Customers.Count() == 51)
			{
				throw new NotSupportedException("The database already contains all 51 customers!");
			}

			Int32 intCustomersAdded = 0;
			String customerEmail = "Begin"; //helps to keep track of error on repos
			List<AppUser> AppUser = new List<AppUser>();

			try
			{
				AppUser u1 = new AppUser();
				u1.UserName = "cbaker@example.com";
				u1.EmailAddress = "cbaker@example.com";
				u1.PhoneNumber = "5725458641";
				u1.FirstName = "Christopher";
				u1.LastName = "Baker";
				u1.Address = "1898 Schurz Alley";
				u1.City = "Austin";
				u1.State = "TX";
				u1.Zip = "78705";
				u1.Password = "bookworm";
				u1.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u1,Customer));
				AppUsers.Add(u1);

				AppUser u2 = new AppUser();
				u2.UserName = "banker@longhorn.net";
				u2.EmailAddress = "banker@longhorn.net";
				u2.PhoneNumber = "9867048435";
				u2.FirstName = "Michelle";
				u2.LastName = "Banks";
				u2.Address = "97 Elmside Pass";
				u2.City = "Austin";
				u2.State = "TX";
				u2.Zip = "78712";
				u2.Password = "potato";
				u2.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u2,Customer));
				AppUsers.Add(u2);

				AppUser u3 = new AppUser();
				u3.UserName = "franco@example.com";
				u3.EmailAddress = "franco@example.com";
				u3.PhoneNumber = "6836109514";
				u3.FirstName = "Franco";
				u3.LastName = "Broccolo";
				u3.Address = "88 Crowley Circle";
				u3.City = "Austin";
				u3.State = "TX";
				u3.Zip = "78786";
				u3.Password = "painting";
				u3.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u3,Customer));
				AppUsers.Add(u3);

				AppUser u4 = new AppUser();
				u4.UserName = "wchang@example.com";
				u4.EmailAddress = "wchang@example.com";
				u4.PhoneNumber = "7070911071";
				u4.FirstName = "Wendy";
				u4.LastName = "Chang";
				u4.Address = "56560 Sage Junction";
				u4.City = "Eagle Pass";
				u4.State = "TX";
				u4.Zip = "78852";
				u4.Password = "texas1";
				u4.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u4,Customer));
				AppUsers.Add(u4);

				AppUser u5 = new AppUser();
				u5.UserName = "limchou@gogle.com";
				u5.EmailAddress = "limchou@gogle.com";
				u5.PhoneNumber = "1488907687";
				u5.FirstName = "Lim";
				u5.LastName = "Chou";
				u5.Address = "60 Lunder Point";
				u5.City = "Austin";
				u5.State = "TX";
				u5.Zip = "78729";
				u5.Password = "Anchorage";
				u5.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u5,Customer));
				AppUsers.Add(u5);

				AppUser u6 = new AppUser();
				u6.UserName = "shdixon@aoll.com";
				u6.EmailAddress = "shdixon@aoll.com";
				u6.PhoneNumber = "6899701824";
				u6.FirstName = "Shan";
				u6.LastName = "Dixon";
				u6.Address = "9448 Pleasure Avenue";
				u6.City = "Georgetown";
				u6.State = "TX";
				u6.Zip = "78628";
				u6.Password = "aggies";
				u6.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u6,Customer));
				AppUsers.Add(u6);

				AppUser u7 = new AppUser();
				u7.UserName = "j.b.evans@aheca.org";
				u7.EmailAddress = "j.b.evans@aheca.org";
				u7.PhoneNumber = "9986825917";
				u7.FirstName = "Jim Bob";
				u7.LastName = "Evans";
				u7.Address = "51 Emmet Parkway";
				u7.City = "Austin";
				u7.State = "TX";
				u7.Zip = "78705";
				u7.Password = "hampton1";
				u7.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u7,Customer));
				AppUsers.Add(u7);

				AppUser u8 = new AppUser();
				u8.UserName = "feeley@penguin.org";
				u8.EmailAddress = "feeley@penguin.org";
				u8.PhoneNumber = "3464121966";
				u8.FirstName = "Lou Ann";
				u8.LastName = "Feeley";
				u8.Address = "65 Darwin Crossing";
				u8.City = "Austin";
				u8.State = "TX";
				u8.Zip = "78704";
				u8.Password = "longhorns";
				u8.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u8,Customer));
				AppUsers.Add(u8);

				AppUser u9 = new AppUser();
				u9.UserName = "tfreeley@minnetonka.ci.us";
				u9.EmailAddress = "tfreeley@minnetonka.ci.us";
				u9.PhoneNumber = "6581357270";
				u9.FirstName = "Tesa";
				u9.LastName = "Freeley";
				u9.Address = "7352 Loftsgordon Court";
				u9.City = "College Station";
				u9.State = "TX";
				u9.Zip = "77840";
				u9.Password = "mustangs";
				u9.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u9,Customer));
				AppUsers.Add(u9);

				AppUser u10 = new AppUser();
				u10.UserName = "mgarcia@gogle.com";
				u10.EmailAddress = "mgarcia@gogle.com";
				u10.PhoneNumber = "3767347949";
				u10.FirstName = "Margaret";
				u10.LastName = "Garcia";
				u10.Address = "7 International Road";
				u10.City = "Austin";
				u10.State = "TX";
				u10.Zip = "78756";
				u10.Password = "onetime";
				u10.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u10,Customer));
				AppUsers.Add(u10);

				AppUser u11 = new AppUser();
				u11.UserName = "chaley@thug.com";
				u11.EmailAddress = "chaley@thug.com";
				u11.PhoneNumber = "2198604221";
				u11.FirstName = "Charles";
				u11.LastName = "Haley";
				u11.Address = "8 Warrior Trail";
				u11.City = "Austin";
				u11.State = "TX";
				u11.Zip = "78746";
				u11.Password = "pepperoni";
				u11.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u11,Customer));
				AppUsers.Add(u11);

				AppUser u12 = new AppUser();
				u12.UserName = "jeffh@sonic.com";
				u12.EmailAddress = "jeffh@sonic.com";
				u12.PhoneNumber = "1222185888";
				u12.FirstName = "Jeffrey";
				u12.LastName = "Hampton";
				u12.Address = "9107 Lighthouse Bay Road";
				u12.City = "Austin";
				u12.State = "TX";
				u12.Zip = "78756";
				u12.Password = "raiders";
				u12.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u12,Customer));
				AppUsers.Add(u12);

				AppUser u13 = new AppUser();
				u13.UserName = "wjhearniii@umich.org";
				u13.EmailAddress = "wjhearniii@umich.org";
				u13.PhoneNumber = "5123071976";
				u13.FirstName = "John";
				u13.LastName = "Hearn";
				u13.Address = "59784 Pierstorff Center";
				u13.City = "Liberty";
				u13.State = "TX";
				u13.Zip = "77575";
				u13.Password = "jhearn22";
				u13.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u13,Customer));
				AppUsers.Add(u13);

				AppUser u14 = new AppUser();
				u14.UserName = "ahick@yaho.com";
				u14.EmailAddress = "ahick@yaho.com";
				u14.PhoneNumber = "1211949601";
				u14.FirstName = "Anthony";
				u14.LastName = "Hicks";
				u14.Address = "932 Monica Way";
				u14.City = "San Antonio";
				u14.State = "TX";
				u14.Zip = "78203";
				u14.Password = "hickhickup";
				u14.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u14,Customer));
				AppUsers.Add(u14);

				AppUser u15 = new AppUser();
				u15.UserName = "ingram@jack.com";
				u15.EmailAddress = "ingram@jack.com";
				u15.PhoneNumber = "1372121569";
				u15.FirstName = "Brad";
				u15.LastName = "Ingram";
				u15.Address = "4 Lukken Court";
				u15.City = "New Braunfels";
				u15.State = "TX";
				u15.Zip = "78132";
				u15.Password = "ingram2015";
				u15.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u15,Customer));
				AppUsers.Add(u15);

				AppUser u16 = new AppUser();
				u16.UserName = "toddj@yourmom.com";
				u16.EmailAddress = "toddj@yourmom.com";
				u16.PhoneNumber = "8543163836";
				u16.FirstName = "Todd";
				u16.LastName = "Jacobs";
				u16.Address = "7 Susan Junction";
				u16.City = "New York";
				u16.State = "NY";
				u16.Zip = "10101";
				u16.Password = "toddy25";
				u16.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u16,Customer));
				AppUsers.Add(u16);

				AppUser u17 = new AppUser();
				u17.UserName = "thequeen@aska.net";
				u17.EmailAddress = "thequeen@aska.net";
				u17.PhoneNumber = "3214163359";
				u17.FirstName = "Victoria";
				u17.LastName = "Lawrence";
				u17.Address = "669 Oak Junction";
				u17.City = "Lockhart";
				u17.State = "TX";
				u17.Zip = "78644";
				u17.Password = "something";
				u17.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u17,Customer));
				AppUsers.Add(u17);

				AppUser u18 = new AppUser();
				u18.UserName = "linebacker@gogle.com";
				u18.EmailAddress = "linebacker@gogle.com";
				u18.PhoneNumber = "2505265350";
				u18.FirstName = "Erik";
				u18.LastName = "Lineback";
				u18.Address = "099 Luster Point";
				u18.City = "Kingwood";
				u18.State = "TX";
				u18.Zip = "77325";
				u18.Password = "Password1";
				u18.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u18,Customer));
				AppUsers.Add(u18);

				AppUser u19 = new AppUser();
				u19.UserName = "elowe@netscare.net";
				u19.EmailAddress = "elowe@netscare.net";
				u19.PhoneNumber = "4070619503";
				u19.FirstName = "Ernest";
				u19.LastName = "Lowe";
				u19.Address = "35473 Hansons Hill";
				u19.City = "Beverly Hills";
				u19.State = "CA";
				u19.Zip = "90210";
				u19.Password = "aclfest2017";
				u19.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u19,Customer));
				AppUsers.Add(u19);

				AppUser u20 = new AppUser();
				u20.UserName = "cluce@gogle.com";
				u20.EmailAddress = "cluce@gogle.com";
				u20.PhoneNumber = "7358436110";
				u20.FirstName = "Chuck";
				u20.LastName = "Luce";
				u20.Address = "4 Emmet Junction";
				u20.City = "Navasota";
				u20.State = "TX";
				u20.Zip = "77868";
				u20.Password = "nothinggood";
				u20.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u20,Customer));
				AppUsers.Add(u20);

				AppUser u21 = new AppUser();
				u21.UserName = "mackcloud@george.com";
				u21.EmailAddress = "mackcloud@george.com";
				u21.PhoneNumber = "7240178229";
				u21.FirstName = "Jennifer";
				u21.LastName = "MacLeod";
				u21.Address = "3 Orin Road";
				u21.City = "Austin";
				u21.State = "TX";
				u21.Zip = "78712";
				u21.Password = "whatever";
				u21.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u21,Customer));
				AppUsers.Add(u21);

				AppUser u22 = new AppUser();
				u22.UserName = "cmartin@beets.com";
				u22.EmailAddress = "cmartin@beets.com";
				u22.PhoneNumber = "2495200223";
				u22.FirstName = "Elizabeth";
				u22.LastName = "Markham";
				u22.Address = "8171 Commercial Crossing";
				u22.City = "Austin";
				u22.State = "TX";
				u22.Zip = "78712";
				u22.Password = "snowsnow";
				u22.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u22,Customer));
				AppUsers.Add(u22);

				AppUser u23 = new AppUser();
				u23.UserName = "clarence@yoho.com";
				u23.EmailAddress = "clarence@yoho.com";
				u23.PhoneNumber = "4086179161";
				u23.FirstName = "Clarence";
				u23.LastName = "Martin";
				u23.Address = "96 Anthes Place";
				u23.City = "Schenectady";
				u23.State = "NY";
				u23.Zip = "12345";
				u23.Password = "whocares";
				u23.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u23,Customer));
				AppUsers.Add(u23);

				AppUser u24 = new AppUser();
				u24.UserName = "gregmartinez@drdre.com";
				u24.EmailAddress = "gregmartinez@drdre.com";
				u24.PhoneNumber = "9371927523";
				u24.FirstName = "Gregory";
				u24.LastName = "Martinez";
				u24.Address = "10 Northridge Plaza";
				u24.City = "Austin";
				u24.State = "TX";
				u24.Zip = "78717";
				u24.Password = "xcellent";
				u24.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u24,Customer));
				AppUsers.Add(u24);

				AppUser u25 = new AppUser();
				u25.UserName = "cmiller@bob.com";
				u25.EmailAddress = "cmiller@bob.com";
				u25.PhoneNumber = "5954063857";
				u25.FirstName = "Charles";
				u25.LastName = "Miller";
				u25.Address = "87683 Schmedeman Circle";
				u25.City = "Austin";
				u25.State = "TX";
				u25.Zip = "78727";
				u25.Password = "mydogspot";
				u25.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u25,Customer));
				AppUsers.Add(u25);

				AppUser u26 = new AppUser();
				u26.UserName = "knelson@aoll.com";
				u26.EmailAddress = "knelson@aoll.com";
				u26.PhoneNumber = "8929209512";
				u26.FirstName = "Kelly";
				u26.LastName = "Nelson";
				u26.Address = "3244 Ludington Court";
				u26.City = "Beaumont";
				u26.State = "TX";
				u26.Zip = "77720";
				u26.Password = "spotmydog";
				u26.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u26,Customer));
				AppUsers.Add(u26);

				AppUser u27 = new AppUser();
				u27.UserName = "joewin@xfactor.com";
				u27.EmailAddress = "joewin@xfactor.com";
				u27.PhoneNumber = "9226301774";
				u27.FirstName = "Joe";
				u27.LastName = "Nguyen";
				u27.Address = "4780 Talisman Court";
				u27.City = "San Marcos";
				u27.State = "TX";
				u27.Zip = "78667";
				u27.Password = "joejoejoe";
				u27.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u27,Customer));
				AppUsers.Add(u27);

				AppUser u28 = new AppUser();
				u28.UserName = "orielly@foxnews.cnn";
				u28.EmailAddress = "orielly@foxnews.cnn";
				u28.PhoneNumber = "2537646912";
				u28.FirstName = "Bill";
				u28.LastName = "O'Reilly";
				u28.Address = "4154 Delladonna Plaza";
				u28.City = "Bergheim";
				u28.State = "TX";
				u28.Zip = "78004";
				u28.Password = "billyboy";
				u28.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u28,Customer));
				AppUsers.Add(u28);

				AppUser u29 = new AppUser();
				u29.UserName = "ankaisrad@gogle.com";
				u29.EmailAddress = "ankaisrad@gogle.com";
				u29.PhoneNumber = "2182889379";
				u29.FirstName = "Anka";
				u29.LastName = "Radkovich";
				u29.Address = "72361 Bayside Drive";
				u29.City = "Austin";
				u29.State = "TX";
				u29.Zip = "78789";
				u29.Password = "radgirl";
				u29.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u29,Customer));
				AppUsers.Add(u29);

				AppUser u30 = new AppUser();
				u30.UserName = "megrhodes@freserve.co.uk";
				u30.EmailAddress = "megrhodes@freserve.co.uk";
				u30.PhoneNumber = "9532396075";
				u30.FirstName = "Megan";
				u30.LastName = "Rhodes";
				u30.Address = "76875 Hoffman Point";
				u30.City = "Orlando";
				u30.State = "FL";
				u30.Zip = "32830";
				u30.Password = "meganr34";
				u30.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u30,Customer));
				AppUsers.Add(u30);

				AppUser u31 = new AppUser();
				u31.UserName = "erynrice@aoll.com";
				u31.EmailAddress = "erynrice@aoll.com";
				u31.PhoneNumber = "7303815953";
				u31.FirstName = "Eryn";
				u31.LastName = "Rice";
				u31.Address = "048 Elmside Park";
				u31.City = "South Padre Island";
				u31.State = "TX";
				u31.Zip = "78597";
				u31.Password = "ricearoni";
				u31.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u31,Customer));
				AppUsers.Add(u31);

				AppUser u32 = new AppUser();
				u32.UserName = "jorge@noclue.com";
				u32.EmailAddress = "jorge@noclue.com";
				u32.PhoneNumber = "3677322422";
				u32.FirstName = "Jorge";
				u32.LastName = "Rodriguez";
				u32.Address = "01 Browning Pass";
				u32.City = "Austin";
				u32.State = "TX";
				u32.Zip = "78744";
				u32.Password = "alaskaboy";
				u32.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u32,Customer));
				AppUsers.Add(u32);

				AppUser u33 = new AppUser();
				u33.UserName = "mrrogers@lovelyday.com";
				u33.EmailAddress = "mrrogers@lovelyday.com";
				u33.PhoneNumber = "3911705385";
				u33.FirstName = "Allen";
				u33.LastName = "Rogers";
				u33.Address = "844 Anderson Alley";
				u33.City = "Canyon Lake";
				u33.State = "TX";
				u33.Zip = "78133";
				u33.Password = "bunnyhop";
				u33.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u33,Customer));
				AppUsers.Add(u33);

				AppUser u34 = new AppUser();
				u34.UserName = "stjean@athome.com";
				u34.EmailAddress = "stjean@athome.com";
				u34.PhoneNumber = "7351610920";
				u34.FirstName = "Olivier";
				u34.LastName = "Saint-Jean";
				u34.Address = "1891 Docker Point";
				u34.City = "Austin";
				u34.State = "TX";
				u34.Zip = "78779";
				u34.Password = "dustydusty";
				u34.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u34,Customer));
				AppUsers.Add(u34);

				AppUser u35 = new AppUser();
				u35.UserName = "saunders@pen.com";
				u35.EmailAddress = "saunders@pen.com";
				u35.PhoneNumber = "5269661692";
				u35.FirstName = "Sarah";
				u35.LastName = "Saunders";
				u35.Address = "1469 Upham Road";
				u35.City = "Austin";
				u35.State = "TX";
				u35.Zip = "78720";
				u35.Password = "jrod2017";
				u35.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u35,Customer));
				AppUsers.Add(u35);

				AppUser u36 = new AppUser();
				u36.UserName = "willsheff@email.com";
				u36.EmailAddress = "willsheff@email.com";
				u36.PhoneNumber = "1875727246";
				u36.FirstName = "William";
				u36.LastName = "Sewell";
				u36.Address = "1672 Oak Valley Circle";
				u36.City = "Austin";
				u36.State = "TX";
				u36.Zip = "78705";
				u36.Password = "martin1234";
				u36.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u36,Customer));
				AppUsers.Add(u36);

				AppUser u37 = new AppUser();
				u37.UserName = "sheffiled@gogle.com";
				u37.EmailAddress = "sheffiled@gogle.com";
				u37.PhoneNumber = "1394323615";
				u37.FirstName = "Martin";
				u37.LastName = "Sheffield";
				u37.Address = "816 Kennedy Place";
				u37.City = "Round Rock";
				u37.State = "TX";
				u37.Zip = "78680";
				u37.Password = "penguin12";
				u37.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u37,Customer));
				AppUsers.Add(u37);

				AppUser u38 = new AppUser();
				u38.UserName = "johnsmith187@aoll.com";
				u38.EmailAddress = "johnsmith187@aoll.com";
				u38.PhoneNumber = "6645937874";
				u38.FirstName = "John";
				u38.LastName = "Smith";
				u38.Address = "0745 Golf Road";
				u38.City = "Austin";
				u38.State = "TX";
				u38.Zip = "78760";
				u38.Password = "rogerthat";
				u38.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u38,Customer));
				AppUsers.Add(u38);

				AppUser u39 = new AppUser();
				u39.UserName = "dustroud@mail.com";
				u39.EmailAddress = "dustroud@mail.com";
				u39.PhoneNumber = "6470254680";
				u39.FirstName = "Dustin";
				u39.LastName = "Stroud";
				u39.Address = "505 Dexter Plaza";
				u39.City = "Sweet Home";
				u39.State = "TX";
				u39.Zip = "77987";
				u39.Password = "smitty444";
				u39.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u39,Customer));
				AppUsers.Add(u39);

				AppUser u40 = new AppUser();
				u40.UserName = "estuart@anchor.net";
				u40.EmailAddress = "estuart@anchor.net";
				u40.PhoneNumber = "7701621022";
				u40.FirstName = "Eric";
				u40.LastName = "Stuart";
				u40.Address = "585 Claremont Drive";
				u40.City = "Corpus Christi";
				u40.State = "TX";
				u40.Zip = "78412";
				u40.Password = "stewball";
				u40.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u40,Customer));
				AppUsers.Add(u40);

				AppUser u41 = new AppUser();
				u41.UserName = "peterstump@noclue.com";
				u41.EmailAddress = "peterstump@noclue.com";
				u41.PhoneNumber = "2181960061";
				u41.FirstName = "Peter";
				u41.LastName = "Stump";
				u41.Address = "89035 Welch Circle";
				u41.City = "Pflugerville";
				u41.State = "TX";
				u41.Zip = "78660";
				u41.Password = "slowwind";
				u41.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u41,Customer));
				AppUsers.Add(u41);

				AppUser u42 = new AppUser();
				u42.UserName = "jtanner@mustang.net";
				u42.EmailAddress = "jtanner@mustang.net";
				u42.PhoneNumber = "9908469499";
				u42.FirstName = "Jeremy";
				u42.LastName = "Tanner";
				u42.Address = "4 Stang Trail";
				u42.City = "Austin";
				u42.State = "TX";
				u42.Zip = "78702";
				u42.Password = "tanner5454";
				u42.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u42,Customer));
				AppUsers.Add(u42);

				AppUser u43 = new AppUser();
				u43.UserName = "taylordjay@aoll.com";
				u43.EmailAddress = "taylordjay@aoll.com";
				u43.PhoneNumber = "7011918647";
				u43.FirstName = "Allison";
				u43.LastName = "Taylor";
				u43.Address = "726 Twin Pines Avenue";
				u43.City = "Austin";
				u43.State = "TX";
				u43.Zip = "78713";
				u43.Password = "allyrally";
				u43.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u43,Customer));
				AppUsers.Add(u43);

				AppUser u44 = new AppUser();
				u44.UserName = "rtaylor@gogle.com";
				u44.EmailAddress = "rtaylor@gogle.com";
				u44.PhoneNumber = "8937910053";
				u44.FirstName = "Rachel";
				u44.LastName = "Taylor";
				u44.Address = "06605 Sugar Drive";
				u44.City = "Austin";
				u44.State = "TX";
				u44.Zip = "78712";
				u44.Password = "taylorbaylor";
				u44.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u44,Customer));
				AppUsers.Add(u44);

				AppUser u45 = new AppUser();
				u45.UserName = "teefrank@noclue.com";
				u45.EmailAddress = "teefrank@noclue.com";
				u45.PhoneNumber = "6394568913";
				u45.FirstName = "Frank";
				u45.LastName = "Tee";
				u45.Address = "3567 Dawn Plaza";
				u45.City = "Austin";
				u45.State = "TX";
				u45.Zip = "78786";
				u45.Password = "teeoff22";
				u45.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u45,Customer));
				AppUsers.Add(u45);

				AppUser u46 = new AppUser();
				u46.UserName = "ctucker@alphabet.co.uk";
				u46.EmailAddress = "ctucker@alphabet.co.uk";
				u46.PhoneNumber = "2676838676";
				u46.FirstName = "Clent";
				u46.LastName = "Tucker";
				u46.Address = "704 Northland Alley";
				u46.City = "San Antonio";
				u46.State = "TX";
				u46.Zip = "78279";
				u46.Password = "tucksack1";
				u46.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u46,Customer));
				AppUsers.Add(u46);

				AppUser u47 = new AppUser();
				u47.UserName = "avelasco@yoho.com";
				u47.EmailAddress = "avelasco@yoho.com";
				u47.PhoneNumber = "3452909754";
				u47.FirstName = "Allen";
				u47.LastName = "Velasco";
				u47.Address = "72 Harbort Point";
				u47.City = "Navasota";
				u47.State = "TX";
				u47.Zip = "77868";
				u47.Password = "meow88";
				u47.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u47,Customer));
				AppUsers.Add(u47);

				AppUser u48 = new AppUser();
				u48.UserName = "vinovino@grapes.com";
				u48.EmailAddress = "vinovino@grapes.com";
				u48.PhoneNumber = "8567089194";
				u48.FirstName = "Janet";
				u48.LastName = "Vino";
				u48.Address = "1 Oak Valley Place";
				u48.City = "Boston";
				u48.State = "MA";
				u48.Zip = "02114";
				u48.Password = "vinovino";
				u48.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u48,Customer));
				AppUsers.Add(u48);

				AppUser u49 = new AppUser();
				u49.UserName = "westj@pioneer.net";
				u49.EmailAddress = "westj@pioneer.net";
				u49.PhoneNumber = "6260784394";
				u49.FirstName = "Jake";
				u49.LastName = "West";
				u49.Address = "48743 Banding Parkway";
				u49.City = "Marble Falls";
				u49.State = "TX";
				u49.Zip = "78654";
				u49.Password = "gowest";
				u49.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u49,Customer));
				AppUsers.Add(u49);

				AppUser u50 = new AppUser();
				u50.UserName = "winner@hootmail.com";
				u50.EmailAddress = "winner@hootmail.com";
				u50.PhoneNumber = "3733971174";
				u50.FirstName = "Louis";
				u50.LastName = "Winthorpe";
				u50.Address = "96850 Summit Crossing";
				u50.City = "Austin";
				u50.State = "TX";
				u50.Zip = "78730";
				u50.Password = "louielouie";
				u50.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u50,Customer));
				AppUsers.Add(u50);

				AppUser u51 = new AppUser();
				u51.UserName = "rwood@voyager.net";
				u51.EmailAddress = "rwood@voyager.net";
				u51.PhoneNumber = "8433359800";
				u51.FirstName = "Reagan";
				u51.LastName = "Wood";
				u51.Address = "18354 Bluejay Street";
				u51.City = "Austin";
				u51.State = "TX";
				u51.Zip = "78712";
				u51.Password = "woodyman1";
				u51.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u51,Customer));
				AppUsers.Add(u51);

			}
				//loop through customers
				foreach (AppUser ur in AppUsers)
				{
					//set name of User to help debug
					customerEmail = ur.EmailAddress;

					//see if repo exists in database
					AppUser dbur = db.AppUsers.FirstOrDefault(r => r.EmailAddress == ur.EmailAddress);

					if (dbur == null) //user does not exist in database
					{
						db.AppUsers.Add(ur);
						db.SaveChanges();
						intUsersAdded += 1;
					}
					else
					{
						dbur.UserName = ur.UserName;
						dbur.EmailAddress = ur.EmailAddress;
						dbur.PhoneNumber = ur.PhoneNumber;
						dbur.FirstName = ur.FirstName;
						dbur.LastName = ur.LastName;
						dbur.Address = ur.Address;
						dbur.City = ur.City;
						dbur.State = ur.State;
						dbur.Zip = ur.Zip;
						dbur.Password = ur.Password;
						db.Update(dbur);
						db.SaveChanges();
					}
				}
			}
			catch
			{
				String msg = "Users added:" + intUsersAdded + "; Error on " + customerEmail;
				throw new InvalidOperationException(msg);
			}
		}
	}
}

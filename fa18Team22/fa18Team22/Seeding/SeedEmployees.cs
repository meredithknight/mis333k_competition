using fa18Team22.Models;
using fa18Team22.DAL;
using System.Collections.Generic;
using System;
using System.Linq;

namespace fa18Team22.Seeding
{
	public static class SeedEmployees
	{
		public static void SeedAllEmployees(AppDbContext db)
		{
			if (db.Employees.Count() == 28)
			{
				throw new NotSupportedException("The database already contains all 28 employees!");
			}

			Int32 intEmployeesAdded = 0;
			String employeeEmail = "Begin"; //helps to keep track of error on repos
			List<AppUser> AppUser = new List<AppUser>();

			try
			{
				AppUser u1 = new AppUser();
				u1.UserName = "c.baker@bevosbooks.com";
				u1.EmailAddress = "c.baker@bevosbooks.com";
				u1.PhoneNumber = "";
				u1.FirstName = "dewey4";
				u1.LastName = "E";
				u1.Address = "1245 Lake Libris Dr.";
				u1.City = "Cedar Park";
				u1.State = "TX";
				u1.Zip = "78613";
				u1.Password = "Christopher";
				u1.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u1,Employee));
				AppUsers.Add(u1);

				AppUser u2 = new AppUser();
				u2.UserName = "s.barnes@bevosbooks.com";
				u2.EmailAddress = "s.barnes@bevosbooks.com";
				u2.PhoneNumber = "";
				u2.FirstName = "smitty";
				u2.LastName = "M";
				u2.Address = "888 S. Main";
				u2.City = "Kyle";
				u2.State = "TX";
				u2.Zip = "78640";
				u2.Password = "Susan";
				u2.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u2,Employee));
				AppUsers.Add(u2);

				AppUser u3 = new AppUser();
				u3.UserName = "h.garcia@bevosbooks.com";
				u3.EmailAddress = "h.garcia@bevosbooks.com";
				u3.PhoneNumber = "";
				u3.FirstName = "squirrel";
				u3.LastName = "W";
				u3.Address = "777 PBR Drive";
				u3.City = "Austin";
				u3.State = "TX";
				u3.Zip = "78712";
				u3.Password = "Hector";
				u3.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u3,Employee));
				AppUsers.Add(u3);

				AppUser u4 = new AppUser();
				u4.UserName = "b.ingram@bevosbooks.com";
				u4.EmailAddress = "b.ingram@bevosbooks.com";
				u4.PhoneNumber = "";
				u4.FirstName = "changalang";
				u4.LastName = "S";
				u4.Address = "6548 La Posada Ct.";
				u4.City = "Austin";
				u4.State = "TX";
				u4.Zip = "78705";
				u4.Password = "Brad";
				u4.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u4,Employee));
				AppUsers.Add(u4);

				AppUser u5 = new AppUser();
				u5.UserName = "j.jackson@bevosbooks.com";
				u5.EmailAddress = "j.jackson@bevosbooks.com";
				u5.PhoneNumber = "";
				u5.FirstName = "rhythm";
				u5.LastName = "J";
				u5.Address = "222 Main";
				u5.City = "Austin";
				u5.State = "TX";
				u5.Zip = "78760";
				u5.Password = "Jack";
				u5.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u5,Employee));
				AppUsers.Add(u5);

				AppUser u6 = new AppUser();
				u6.UserName = "t.jacobs@bevosbooks.com";
				u6.EmailAddress = "t.jacobs@bevosbooks.com";
				u6.PhoneNumber = "";
				u6.FirstName = "approval";
				u6.LastName = "L";
				u6.Address = "4564 Elm St.";
				u6.City = "Georgetown";
				u6.State = "TX";
				u6.Zip = "78628";
				u6.Password = "Todd";
				u6.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u6,Employee));
				AppUsers.Add(u6);

				AppUser u7 = new AppUser();
				u7.UserName = "l.jones@bevosbooks.com";
				u7.EmailAddress = "l.jones@bevosbooks.com";
				u7.PhoneNumber = "";
				u7.FirstName = "society";
				u7.LastName = "L";
				u7.Address = "999 LeBlat";
				u7.City = "Austin";
				u7.State = "TX";
				u7.Zip = "78747";
				u7.Password = "Lester";
				u7.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u7,Employee));
				AppUsers.Add(u7);

				AppUser u8 = new AppUser();
				u8.UserName = "b.larson@bevosbooks.com";
				u8.EmailAddress = "b.larson@bevosbooks.com";
				u8.PhoneNumber = "";
				u8.FirstName = "tanman";
				u8.LastName = "B";
				u8.Address = "1212 N. First Ave";
				u8.City = "Round Rock";
				u8.State = "TX";
				u8.Zip = "78665";
				u8.Password = "Bill";
				u8.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u8,Employee));
				AppUsers.Add(u8);

				AppUser u9 = new AppUser();
				u9.UserName = "v.lawrence@bevosbooks.com";
				u9.EmailAddress = "v.lawrence@bevosbooks.com";
				u9.PhoneNumber = "";
				u9.FirstName = "longhorns";
				u9.LastName = "Y";
				u9.Address = "6639 Bookworm Ln.";
				u9.City = "Austin";
				u9.State = "TX";
				u9.Zip = "78712";
				u9.Password = "Victoria";
				u9.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u9,Employee));
				AppUsers.Add(u9);

				AppUser u10 = new AppUser();
				u10.UserName = "m.lopez@bevosbooks.com";
				u10.EmailAddress = "m.lopez@bevosbooks.com";
				u10.PhoneNumber = "";
				u10.FirstName = "swansong";
				u10.LastName = "T";
				u10.Address = "90 SW North St";
				u10.City = "Austin";
				u10.State = "TX";
				u10.Zip = "78729";
				u10.Password = "Marshall";
				u10.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u10,Employee));
				AppUsers.Add(u10);

				AppUser u11 = new AppUser();
				u11.UserName = "j.macleod@bevosbooks.com";
				u11.EmailAddress = "j.macleod@bevosbooks.com";
				u11.PhoneNumber = "";
				u11.FirstName = "fungus";
				u11.LastName = "D";
				u11.Address = "2504 Far West Blvd.";
				u11.City = "Austin";
				u11.State = "TX";
				u11.Zip = "78705";
				u11.Password = "Jennifer";
				u11.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u11,Employee));
				AppUsers.Add(u11);

				AppUser u12 = new AppUser();
				u12.UserName = "e.markham@bevosbooks.com";
				u12.EmailAddress = "e.markham@bevosbooks.com";
				u12.PhoneNumber = "";
				u12.FirstName = "median";
				u12.LastName = "K";
				u12.Address = "7861 Chevy Chase";
				u12.City = "Austin";
				u12.State = "TX";
				u12.Zip = "78785";
				u12.Password = "Elizabeth";
				u12.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u12,Employee));
				AppUsers.Add(u12);

				AppUser u13 = new AppUser();
				u13.UserName = "g.martinez@bevosbooks.com";
				u13.EmailAddress = "g.martinez@bevosbooks.com";
				u13.PhoneNumber = "";
				u13.FirstName = "decorate";
				u13.LastName = "R";
				u13.Address = "8295 Sunset Blvd.";
				u13.City = "Austin";
				u13.State = "TX";
				u13.Zip = "78712";
				u13.Password = "Gregory";
				u13.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u13,Employee));
				AppUsers.Add(u13);

				AppUser u14 = new AppUser();
				u14.UserName = "j.mason@bevosbooks.com";
				u14.EmailAddress = "j.mason@bevosbooks.com";
				u14.PhoneNumber = "";
				u14.FirstName = "rankmary";
				u14.LastName = "L";
				u14.Address = "444 45th St";
				u14.City = "Austin";
				u14.State = "TX";
				u14.Zip = "78701";
				u14.Password = "Jack";
				u14.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u14,Employee));
				AppUsers.Add(u14);

				AppUser u15 = new AppUser();
				u15.UserName = "c.miller@bevosbooks.com";
				u15.EmailAddress = "c.miller@bevosbooks.com";
				u15.PhoneNumber = "";
				u15.FirstName = "kindly";
				u15.LastName = "R";
				u15.Address = "8962 Main St.";
				u15.City = "Austin";
				u15.State = "TX";
				u15.Zip = "78709";
				u15.Password = "Charles";
				u15.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u15,Employee));
				AppUsers.Add(u15);

				AppUser u16 = new AppUser();
				u16.UserName = "m.nguyen@bevosbooks.com";
				u16.EmailAddress = "m.nguyen@bevosbooks.com";
				u16.PhoneNumber = "";
				u16.FirstName = "ricearoni";
				u16.LastName = "J";
				u16.Address = "465 N. Bear Cub";
				u16.City = "Austin";
				u16.State = "TX";
				u16.Zip = "78734";
				u16.Password = "Mary";
				u16.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u16,Employee));
				AppUsers.Add(u16);

				AppUser u17 = new AppUser();
				u17.UserName = "s.rankin@bevosbooks.com";
				u17.EmailAddress = "s.rankin@bevosbooks.com";
				u17.PhoneNumber = "";
				u17.FirstName = "walkamile";
				u17.LastName = "R";
				u17.Address = "23 Dewey Road";
				u17.City = "Austin";
				u17.State = "TX";
				u17.Zip = "78712";
				u17.Password = "Suzie";
				u17.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u17,Employee));
				AppUsers.Add(u17);

				AppUser u18 = new AppUser();
				u18.UserName = "m.rhodes@bevosbooks.com";
				u18.EmailAddress = "m.rhodes@bevosbooks.com";
				u18.PhoneNumber = "";
				u18.FirstName = "ingram45";
				u18.LastName = "C";
				u18.Address = "4587 Enfield Rd.";
				u18.City = "Austin";
				u18.State = "TX";
				u18.Zip = "78729";
				u18.Password = "Megan";
				u18.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u18,Employee));
				AppUsers.Add(u18);

				AppUser u19 = new AppUser();
				u19.UserName = "e.rice@bevosbooks.com";
				u19.EmailAddress = "e.rice@bevosbooks.com";
				u19.PhoneNumber = "";
				u19.FirstName = "arched";
				u19.LastName = "M";
				u19.Address = "3405 Rio Grande";
				u19.City = "Austin";
				u19.State = "TX";
				u19.Zip = "78746";
				u19.Password = "Eryn";
				u19.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u19,Employee));
				AppUsers.Add(u19);

				AppUser u20 = new AppUser();
				u20.UserName = "a.rogers@bevosbooks.com";
				u20.EmailAddress = "a.rogers@bevosbooks.com";
				u20.PhoneNumber = "";
				u20.FirstName = "lottery";
				u20.LastName = "H";
				u20.Address = "4965 Oak Hill";
				u20.City = "Austin";
				u20.State = "TX";
				u20.Zip = "78705";
				u20.Password = "Allen";
				u20.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u20,Employee));
				AppUsers.Add(u20);

				AppUser u21 = new AppUser();
				u21.UserName = "s.saunders@bevosbooks.com";
				u21.EmailAddress = "s.saunders@bevosbooks.com";
				u21.PhoneNumber = "";
				u21.FirstName = "nostalgic";
				u21.LastName = "M";
				u21.Address = "332 Avenue C";
				u21.City = "Austin";
				u21.State = "TX";
				u21.Zip = "78733";
				u21.Password = "Sarah";
				u21.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u21,Employee));
				AppUsers.Add(u21);

				AppUser u22 = new AppUser();
				u22.UserName = "w.sewell@bevosbooks.com";
				u22.EmailAddress = "w.sewell@bevosbooks.com";
				u22.PhoneNumber = "";
				u22.FirstName = "offbeat";
				u22.LastName = "G";
				u22.Address = "2365 51st St.";
				u22.City = "Austin";
				u22.State = "TX";
				u22.Zip = "78755";
				u22.Password = "William";
				u22.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u22,Employee));
				AppUsers.Add(u22);

				AppUser u23 = new AppUser();
				u23.UserName = "m.sheffield@bevosbooks.com";
				u23.EmailAddress = "m.sheffield@bevosbooks.com";
				u23.PhoneNumber = "";
				u23.FirstName = "evanescent";
				u23.LastName = "J";
				u23.Address = "3886 Avenue A";
				u23.City = "San Marcos";
				u23.State = "TX";
				u23.Zip = "78666";
				u23.Password = "Martin";
				u23.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u23,Employee));
				AppUsers.Add(u23);

				AppUser u24 = new AppUser();
				u24.UserName = "c.silva@bevosbooks.com";
				u24.EmailAddress = "c.silva@bevosbooks.com";
				u24.PhoneNumber = "";
				u24.FirstName = "stewboy";
				u24.LastName = "S";
				u24.Address = "900 4th St";
				u24.City = "Austin";
				u24.State = "TX";
				u24.Zip = "78758";
				u24.Password = "Cindy";
				u24.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u24,Employee));
				AppUsers.Add(u24);

				AppUser u25 = new AppUser();
				u25.UserName = "e.stuart@bevosbooks.com";
				u25.EmailAddress = "e.stuart@bevosbooks.com";
				u25.PhoneNumber = "";
				u25.FirstName = "instrument";
				u25.LastName = "F";
				u25.Address = "5576 Toro Ring";
				u25.City = "Austin";
				u25.State = "TX";
				u25.Zip = "78758";
				u25.Password = "Eric";
				u25.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u25,Employee));
				AppUsers.Add(u25);

				AppUser u26 = new AppUser();
				u26.UserName = "j.tanner@bevosbooks.com";
				u26.EmailAddress = "j.tanner@bevosbooks.com";
				u26.PhoneNumber = "";
				u26.FirstName = "hecktour";
				u26.LastName = "S";
				u26.Address = "4347 Almstead";
				u26.City = "Austin";
				u26.State = "TX";
				u26.Zip = "78712";
				u26.Password = "Jeremy";
				u26.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u26,Employee));
				AppUsers.Add(u26);

				AppUser u27 = new AppUser();
				u27.UserName = "a.taylor@bevosbooks.com";
				u27.EmailAddress = "a.taylor@bevosbooks.com";
				u27.PhoneNumber = "";
				u27.FirstName = "countryrhodes";
				u27.LastName = "R";
				u27.Address = "467 Nueces St.";
				u27.City = "Austin";
				u27.State = "TX";
				u27.Zip = "78727";
				u27.Password = "Allison";
				u27.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u27,Employee));
				AppUsers.Add(u27);

				AppUser u28 = new AppUser();
				u28.UserName = "r.taylor@bevosbooks.com";
				u28.EmailAddress = "r.taylor@bevosbooks.com";
				u28.PhoneNumber = "";
				u28.FirstName = "landus";
				u28.LastName = "O";
				u28.Address = "345 Longview Dr.";
				u28.City = "Austin";
				u28.State = "TX";
				u28.Zip = "78746";
				u28.Password = "Rachel";
				u28.UserStatus = "Active";
				if (await _userManager.IsInRoleAsync(u28,Employee));
				AppUsers.Add(u28);

			}
				//loop through employees
				foreach (AppUser ur in AppUsers)
				{
					//set name of User to help debug
					employeeEmail = ur.EmailAddress;

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
				String msg = "Users added:" + intUsersAdded + "; Error on " + employeeEmail;
				throw new InvalidOperationException(msg);
			}
		}
	}
}

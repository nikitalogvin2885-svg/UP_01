using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HousingUtilitiesAccounting
{
    // Класс для представления услуги ЖКХ
    public class UtilityService
    {
        public int Id { get; set; }
        public string Name { get; set; }          // Например: "Холодная вода", "Отопление", "Электроэнергия"
        public string Unit { get; set; }           // Единица измерения: м³, кВт·ч, Гкал и т.д.
        public decimal Rate { get; set; }          // Тариф за единицу (руб.)

        public override string ToString()
        {
            return $"ID: {Id,-3} | {Name,-25} | Ед.изм.: {Unit,-8} | Тариф: {Rate,8:C}";
        }
    }

    // Класс для представления собственника
    public class Owner
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"ID: {Id,-3} | {FullName,-30} | Телефон: {Phone}";
        }
    }

    // Класс для представления жилого помещения (квартиры)
    public class Apartment
    {
        public int Id { get; set; }
        public string Address { get; set; }        // Например: "ул. Ленина, д.5, кв.12"
        public decimal Area { get; set; }          // Площадь в м²
        public int ResidentsCount { get; set; }    // Количество проживающих
        public Owner Owner { get; set; }

        public override string ToString()
        {
            return $"ID: {Id,-3} | {Address,-30} | Площадь: {Area,5} м² | Жильцов: {ResidentsCount,2} | Собственник: {Owner?.FullName ?? "—"}";
        }
    }

    // Класс для начисления по услуге за период
    public class ChargeItem
    {
        public UtilityService Service { get; set; }
        public decimal Consumption { get; set; }   // Потребление (по счётчику или норматив)
        public decimal Amount { get; set; }        // Начисленная сумма = Consumption * Rate

        public override string ToString()
        {
            return $" {Service.Name,-25} | {Consumption,8} {Service.Unit} | {Amount,10:C}";
        }
    }

    // Класс для начисления за месяц по квартире
    public class MonthlyCharge
    {
        public int Id { get; set; }
        public Apartment Apartment { get; set; }
        public DateTime Period { get; set; }       // Например: 01.12.2025 (период начисления)
        public List<ChargeItem> Items { get; set; }
        public decimal TotalAmount { get; set; }

        public MonthlyCharge()
        {
            Items = new List<ChargeItem>();
        }

        public override string ToString()
        {
            return $"Начисление #{Id} | Квартира: {Apartment.Id} | Период: {Period:MM.yyyy} | Сумма: {TotalAmount:C}";
        }
    }

    // Класс для оплаты
    public class Payment
    {
        public int Id { get; set; }
        public Apartment Apartment { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }    // Например: "Оплата за декабрь 2025"

        public override string ToString()
        {
            return $"Оплата #{Id} | Квартира: {Apartment.Id} | Дата: {PaymentDate:dd.MM.yyyy} | Сумма: {Amount:C} | {Description}";
        }
    }

    // Главный класс системы
    public class HousingUtilitiesSystem
    {
        private List<Apartment> apartments;
        private List<Owner> owners;
        private List<UtilityService> services;
        private List<MonthlyCharge> charges;
        private List<Payment> payments;

        private int nextApartmentId = 1;
        private int nextOwnerId = 1;
        private int nextServiceId = 1;
        private int nextChargeId = 1;
        private int nextPaymentId = 1;

        public HousingUtilitiesSystem()
        {
            apartments = new List<Apartment>();
            owners = new List<Owner>();
            services = new List<UtilityService>();
            charges = new List<MonthlyCharge>();
            payments = new List<Payment>();

            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Примерные собственники
            owners.Add(new Owner { Id = nextOwnerId++, FullName = "Иванов Иван Иванович", Phone = "+7-900-111-22-33" });
            owners.Add(new Owner { Id = nextOwnerId++, FullName = "Петрова Анна Сергеевна", Phone = "+7-900-444-55-66" });
            owners.Add(new Owner { Id = nextOwnerId++, FullName = "Сидоров Пётр Михайлович", Phone = "+7-900-777-88-99" });

            // Примерные квартиры
            apartments.Add(new Apartment { Id = nextApartmentId++, Address = "ул. Ленина, д.10, кв.5", Area = 65.5m, ResidentsCount = 3, Owner = owners[0] });
            apartments.Add(new Apartment { Id = nextApartmentId++, Address = "ул. Ленина, д.10, кв.12", Area = 45.0m, ResidentsCount = 2, Owner = owners[1] });
            apartments.Add(new Apartment { Id = nextApartmentId++, Address = "ул. Мира, д.3, кв.8", Area = 80.0m, ResidentsCount = 4, Owner = owners[2] });

            // Примерные услуги и тарифы
            services.Add(new UtilityService { Id = nextServiceId++, Name = "Холодная вода", Unit = "м³", Rate = 45.20m });
            services.Add(new UtilityService { Id = nextServiceId++, Name = "Горячая вода", Unit = "м³", Rate = 180.50m });
            services.Add(new UtilityService { Id = nextServiceId++, Name = "Отопление", Unit = "Гкал", Rate = 2200.00m });
            services.Add(new UtilityService { Id = nextServiceId++, Name = "Электроэнергия", Unit = "кВт·ч", Rate = 5.80m });
            services.Add(new UtilityService { Id = nextServiceId++, Name = "Содержание жилья", Unit = "м²", Rate = 35.00m });
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("════════════════════════════════════════════════════════════");
                Console.WriteLine("   ИНФОРМАЦИОННАЯ СИСТЕМА УЧЁТА ПЛАТЕЖЕЙ ЖКХ");
                Console.WriteLine("════════════════════════════════════════════════════════════");
                Console.WriteLine();
                Console.WriteLine("1. Реестр помещений и собственников");
                Console.WriteLine("2. Управление тарифами");
                Console.WriteLine("3. Начисления и оплаты");
                Console.WriteLine("4. Отчёты и задолженность");
                Console.WriteLine("5. Выход");
                Console.WriteLine();
                Console.Write("Выберите пункт меню (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ApartmentsAndOwnersManagement();
                        break;
                    case "2":
                        ServicesManagement();
                        break;
                    case "3":
                        ChargesAndPaymentsManagement();
                        break;
                    case "4":
                        Reports();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // 1. Реестр помещений и собственников
        private void ApartmentsAndOwnersManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine(" РЕЕСТР ПОМЕЩЕНИЙ И СОБСТВЕННИКОВ");
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine();
                Console.WriteLine("1. Просмотреть все квартиры");
                Console.WriteLine("2. Добавить квартиру");
                Console.WriteLine("3. Обновить квартиру");
                Console.WriteLine("4. Удалить квартиру");
                Console.WriteLine("5. Просмотреть всех собственников");
                Console.WriteLine("6. Добавить собственника");
                Console.WriteLine("7. Вернуться в главное меню");
                Console.WriteLine();
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllApartments(); break;
                    case "2": AddApartment(); break;
                    case "3": UpdateApartment(); break;
                    case "4": DeleteApartment(); break;
                    case "5": ViewAllOwners(); break;
                    case "6": AddOwner(); break;
                    case "7": managing = false; break;
                    default:
                        Console.WriteLine("Неверный выбор."); Console.ReadLine(); break;
                }
            }
        }

        private void ViewAllApartments()
        {
            Console.Clear();
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            Console.WriteLine(" ВСЕ КВАРТИРЫ");
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            if (apartments.Count == 0) Console.WriteLine("Квартир нет.");
            else foreach (var apt in apartments) Console.WriteLine(apt);
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void AddApartment()
        {
            Console.Clear();
            Console.WriteLine(" ДОБАВИТЬ КВАРТИРУ");
            Console.Write("Адрес: "); string address = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(address)) { Console.WriteLine("Ошибка."); Console.ReadLine(); return; }

            Console.Write("Площадь (м²): "); if (!decimal.TryParse(Console.ReadLine(), out decimal area) || area <= 0) { Console.WriteLine("Ошибка."); Console.ReadLine(); return; }
            Console.Write("Кол-во жильцов: "); if (!int.TryParse(Console.ReadLine(), out int residents) || residents < 0) { Console.WriteLine("Ошибка."); Console.ReadLine(); return; }

            ViewAllOwners();
            Console.Write("ID собственника (0 — без собственника): "); int ownerId = int.TryParse(Console.ReadLine(), out int oid) ? oid : -1;
            Owner owner = owners.FirstOrDefault(o => o.Id == ownerId);

            apartments.Add(new Apartment
            {
                Id = nextApartmentId++,
                Address = address,
                Area = area,
                ResidentsCount = residents,
                Owner = owner
            });
            Console.WriteLine("✓ Квартира добавлена."); Console.ReadLine();
        }

        private void UpdateApartment()
        {
            Console.Write("ID квартиры: "); if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var apt = apartments.FirstOrDefault(a => a.Id == id);
            if (apt == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }

            Console.WriteLine($"Текущие данные: {apt}");
            Console.Write("Новый адрес (Enter — пропустить): "); string addr = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(addr)) apt.Address = addr;
            Console.Write("Новая площадь (Enter — пропустить): "); string areaStr = Console.ReadLine(); if (decimal.TryParse(areaStr, out decimal area)) apt.Area = area;
            Console.Write("Новое кол-во жильцов (Enter — пропустить): "); string resStr = Console.ReadLine(); if (int.TryParse(resStr, out int res)) apt.ResidentsCount = res;

            ViewAllOwners();
            Console.Write("Новый ID собственника (Enter — пропустить): "); string ownerStr = Console.ReadLine();
            if (int.TryParse(ownerStr, out int oid)) apt.Owner = owners.FirstOrDefault(o => o.Id == oid);

            Console.WriteLine("✓ Обновлено."); Console.ReadLine();
        }

        private void DeleteApartment()
        {
            Console.Write("ID квартиры для удаления: "); if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var apt = apartments.FirstOrDefault(a => a.Id == id);
            if (apt == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }
            Console.Write($"Удалить {apt.Address}? (д/н): ");
            if (Console.ReadLine().ToLower() == "д")
            {
                apartments.Remove(apt);
                Console.WriteLine("✓ Удалено.");
            }
            Console.ReadLine();
        }

        private void ViewAllOwners()
        {
            Console.Clear();
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            Console.WriteLine(" ВСЕ СОБСТВЕННИКИ");
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            if (owners.Count == 0) Console.WriteLine("Собственников нет.");
            else foreach (var owner in owners) Console.WriteLine(owner);
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void AddOwner()
        {
            Console.Clear();
            Console.Write("ФИО: "); string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Ошибка."); Console.ReadLine(); return; }
            Console.Write("Телефон: "); string phone = Console.ReadLine();

            owners.Add(new Owner { Id = nextOwnerId++, FullName = name, Phone = phone });
            Console.WriteLine("✓ Собственник добавлен."); Console.ReadLine();
        }

        // 2. Управление тарифами
        private void ServicesManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine(" УПРАВЛЕНИЕ ТАРИФАМИ НА УСЛУГИ");
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine();
                Console.WriteLine("1. Просмотреть все услуги");
                Console.WriteLine("2. Добавить услугу");
                Console.WriteLine("3. Обновить тариф");
                Console.WriteLine("4. Удалить услугу");
                Console.WriteLine("5. Вернуться");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllServices(); break;
                    case "2": AddService(); break;
                    case "3": UpdateServiceRate(); break;
                    case "4": DeleteService(); break;
                    case "5": managing = false; break;
                    default: Console.WriteLine("Неверно."); Console.ReadLine(); break;
                }
            }
        }

        private void ViewAllServices()
        {
            Console.Clear();
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            Console.WriteLine(" ВСЕ УСЛУГИ И ТАРИФЫ");
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            if (services.Count == 0) Console.WriteLine("Услуг нет.");
            else foreach (var s in services) Console.WriteLine(s);
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void AddService()
        {
            Console.Clear();
            Console.Write("Название услуги: "); string name = Console.ReadLine();
            Console.Write("Единица измерения: "); string unit = Console.ReadLine();
            Console.Write("Тариф: "); if (!decimal.TryParse(Console.ReadLine(), out decimal rate) || rate < 0) { Console.WriteLine("Ошибка."); Console.ReadLine(); return; }

            services.Add(new UtilityService { Id = nextServiceId++, Name = name, Unit = unit, Rate = rate });
            Console.WriteLine("✓ Услуга добавлена."); Console.ReadLine();
        }

        private void UpdateServiceRate()
        {
            ViewAllServices();
            Console.Write("ID услуги для изменения тарифа: "); if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var service = services.FirstOrDefault(s => s.Id == id);
            if (service == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }

            Console.Write("Новый тариф: "); if (decimal.TryParse(Console.ReadLine(), out decimal rate) && rate >= 0) service.Rate = rate;
            Console.WriteLine("✓ Тариф обновлён."); Console.ReadLine();
        }

        private void DeleteService()
        {
            ViewAllServices();
            Console.Write("ID услуги для удаления: "); if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var service = services.FirstOrDefault(s => s.Id == id);
            if (service == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }
            Console.Write($"Удалить {service.Name}? (д/н): ");
            if (Console.ReadLine().ToLower() == "д") { services.Remove(service); Console.WriteLine("✓ Удалено."); }
            Console.ReadLine();
        }

        // 3. Начисления и оплаты
        private void ChargesAndPaymentsManagement()
        {
            bool managing = true;
            while (managing)
            {
                Console.Clear();
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine(" НАЧИСЛЕНИЯ И ОПЛАТЫ");
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine();
                Console.WriteLine("1. Создать начисление за период");
                Console.WriteLine("2. Зарегистрировать оплату");
                Console.WriteLine("3. Просмотреть начисления");
                Console.WriteLine("4. Просмотреть оплаты");
                Console.WriteLine("5. Печать квитанции");
                Console.WriteLine("6. Вернуться");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": CreateMonthlyCharge(); break;
                    case "2": RegisterPayment(); break;
                    case "3": ViewAllCharges(); break;
                    case "4": ViewAllPayments(); break;
                    case "5": PrintReceipt(); break;
                    case "6": managing = false; break;
                    default: Console.WriteLine("Неверно."); Console.ReadLine(); break;
                }
            }
        }

        private void CreateMonthlyCharge()
        {
            Console.Clear();
            ViewAllApartments();
            Console.Write("ID квартиры: "); if (!int.TryParse(Console.ReadLine(), out int aptId)) return;
            var apartment = apartments.FirstOrDefault(a => a.Id == aptId);
            if (apartment == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }

            Console.Write("Период начисления (мм.гггг, например 12.2025): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime period))
            {
                Console.WriteLine("Неверный формат."); Console.ReadLine(); return;
            }
            period = new DateTime(period.Year, period.Month, 1);

            var charge = new MonthlyCharge { Id = nextChargeId++, Apartment = apartment, Period = period };

            ViewAllServices();
            bool adding = true;
            while (adding)
            {
                Console.Write("ID услуги (0 — завершить): ");
                if (!int.TryParse(Console.ReadLine(), out int serviceId) || serviceId == 0)
                {
                    if (charge.Items.Count == 0) { Console.WriteLine("Добавьте хотя бы одну услугу."); Console.ReadLine(); continue; }
                    adding = false; break;
                }

                var service = services.FirstOrDefault(s => s.Id == serviceId);
                if (service == null) { Console.WriteLine("Услуга не найдена."); Console.ReadLine(); continue; }

                Console.Write($"Потребление {service.Name} ({service.Unit}): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal consumption) || consumption < 0)
                {
                    Console.WriteLine("Неверное значение."); Console.ReadLine(); continue;
                }

                decimal amount = consumption * service.Rate;
                charge.Items.Add(new ChargeItem { Service = service, Consumption = consumption, Amount = amount });
                Console.WriteLine($"Добавлено: {amount:C}");
            }

            charge.TotalAmount = charge.Items.Sum(i => i.Amount);
            charges.Add(charge);
            Console.WriteLine($"✓ Начисление создано. Итого: {charge.TotalAmount:C}");
            Console.ReadLine();
        }

        private void RegisterPayment()
        {
            Console.Clear();
            ViewAllApartments();
            Console.Write("ID квартиры: "); if (!int.TryParse(Console.ReadLine(), out int aptId)) return;
            var apartment = apartments.FirstOrDefault(a => a.Id == aptId);
            if (apartment == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }

            Console.Write("Сумма оплаты: "); if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0) { Console.WriteLine("Ошибка."); Console.ReadLine(); return; }
            Console.Write("Описание (например, «За декабрь 2025»): "); string desc = Console.ReadLine();

            payments.Add(new Payment
            {
                Id = nextPaymentId++,
                Apartment = apartment,
                PaymentDate = DateTime.Now,
                Amount = amount,
                Description = desc ?? ""
            });
            Console.WriteLine("✓ Оплата зарегистрирована."); Console.ReadLine();
        }

        private void ViewAllCharges()
        {
            Console.Clear();
            Console.WriteLine(" ВСЕ НАЧИСЛЕНИЯ");
            if (charges.Count == 0) Console.WriteLine("Нет начислений.");
            else foreach (var c in charges.OrderByDescending(c => c.Period)) Console.WriteLine(c);
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void ViewAllPayments()
        {
            Console.Clear();
            Console.WriteLine(" ВСЕ ОПЛАТЫ");
            if (payments.Count == 0) Console.WriteLine("Нет оплат.");
            else foreach (var p in payments.OrderByDescending(p => p.PaymentDate)) Console.WriteLine(p);
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void PrintReceipt()
        {
            Console.Write("ID начисления для печати квитанции: "); if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var charge = charges.FirstOrDefault(c => c.Id == id);
            if (charge == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }

            Console.Clear();
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.WriteLine("                КВИТАНЦИЯ НА ОПЛАТУ ЖКХ");
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.WriteLine($"Квартира: {charge.Apartment.Address}");
            Console.WriteLine($"Собственник: {charge.Apartment.Owner?.FullName ?? "—"}");
            Console.WriteLine($"Период: {charge.Period:MMMM yyyy}");
            Console.WriteLine($"Площадь: {charge.Apartment.Area} м² | Жильцов: {charge.Apartment.ResidentsCount}");
            Console.WriteLine();
            Console.WriteLine("Начисления:");
            Console.WriteLine(new string('-', 60));
            foreach (var item in charge.Items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"ИТОГО К ОПЛАТЕ: {charge.TotalAmount,45:C}");
            Console.WriteLine();
            Console.WriteLine("Оплатить до: " + charge.Period.AddMonths(1).AddDays(-1).ToString("dd.MM.yyyy"));
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.WriteLine("\nНажмите Enter для возврата...");
            Console.ReadLine();
        }

        // 4. Отчёты и задолженность
        private void Reports()
        {
            bool viewing = true;
            while (viewing)
            {
                Console.Clear();
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine(" ОТЧЁТЫ И ЗАДОЛЖЕННОСТЬ");
                Console.WriteLine("════════════════════════════════════════════════");
                Console.WriteLine();
                Console.WriteLine("1. Задолженность по всем квартирам");
                Console.WriteLine("2. Задолженность по конкретной квартире");
                Console.WriteLine("3. Общая статистика");
                Console.WriteLine("4. Вернуться");
                Console.Write("Выберите отчёт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DebtReportAll(); break;
                    case "2": DebtReportByApartment(); break;
                    case "3": GeneralStats(); break;
                    case "4": viewing = false; break;
                    default: Console.WriteLine("Неверно."); Console.ReadLine(); break;
                }
            }
        }

        private decimal CalculateDebt(Apartment apartment)
        {
            decimal charged = charges.Where(c => c.Apartment.Id == apartment.Id).Sum(c => c.TotalAmount);
            decimal paid = payments.Where(p => p.Apartment.Id == apartment.Id).Sum(p => p.Amount);
            return charged - paid;
        }

        private void DebtReportAll()
        {
            Console.Clear();
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            Console.WriteLine(" ЗАДОЛЖЕННОСТЬ ПО ВСЕМ КВАРТИРАМ");
            Console.WriteLine("════════════════════════════════════════════════════════════════");
            decimal totalDebt = 0;
            foreach (var apt in apartments)
            {
                decimal debt = CalculateDebt(apt);
                if (debt > 0)
                {
                    Console.WriteLine($"Кв. {apt.Id,-3} | {apt.Address,-30} | Задолженность: {debt,10:C}");
                    totalDebt += debt;
                }
            }
            if (totalDebt == 0) Console.WriteLine("Задолженности нет.");
            else Console.WriteLine($"\nОбщая задолженность: {totalDebt:C}");
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void DebtReportByApartment()
        {
            ViewAllApartments();
            Console.Write("ID квартиры: "); if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var apt = apartments.FirstOrDefault(a => a.Id == id);
            if (apt == null) { Console.WriteLine("Не найдено."); Console.ReadLine(); return; }

            decimal debt = CalculateDebt(apt);
            Console.Clear();
            Console.WriteLine($"ЗАДОЛЖЕННОСТЬ ПО КВАРТИРЕ {apt.Address}");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Начислено всего: {charges.Where(c => c.Apartment.Id == id).Sum(c => c.TotalAmount):C}");
            Console.WriteLine($"Оплачено всего:  {payments.Where(p => p.Apartment.Id == id).Sum(p => p.Amount):C}");
            Console.WriteLine($"Текущая задолженность: {debt:C}");
            if (debt > 0) Console.WriteLine("\n⚠ Внимание: есть долг!");
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }

        private void GeneralStats()
        {
            Console.Clear();
            Console.WriteLine(" ОБЩАЯ СТАТИСТИКА");
            Console.WriteLine("════════════════════════════════════════════════");
            Console.WriteLine($"Квартир в реестре: {apartments.Count}");
            Console.WriteLine($"Собственников: {owners.Count}");
            Console.WriteLine($"Услуг: {services.Count}");
            Console.WriteLine($"Начислений: {charges.Count}");
            Console.WriteLine($"Оплат: {payments.Count}");
            Console.WriteLine($"Общая сумма начислений: {charges.Sum(c => c.TotalAmount):C}");
            Console.WriteLine($"Общая сумма оплат: {payments.Sum(p => p.Amount):C}");
            decimal totalDebt = apartments.Sum(a => Math.Max(0, CalculateDebt(a)));
            Console.WriteLine($"Общая задолженность жильцов: {totalDebt:C}");
            Console.WriteLine("\nНажмите Enter..."); Console.ReadLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var system = new HousingUtilitiesSystem();
            system.Run();
        }
    }
}
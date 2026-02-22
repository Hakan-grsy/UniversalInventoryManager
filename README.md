# 📦 Universal Inventory Manager

**[EN]** A modern, layered desktop application built with C# and WPF to manage diverse product inventories. 
**[TR]** C# ve WPF kullanılarak geliştirilmiş, farklı ürün tiplerini dinamik formlarla yönetebilen katmanlı mimariye sahip modern masaüstü stok yönetim otomasyonu.

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![.Net](https://img.shields.io/badge/.NET_8-5C2D91?style=for-the-badge&logo=.net&logoColor=white) ![SQLite](https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white) ![WPF](https://img.shields.io/badge/WPF-Blue?style=for-the-badge&logo=windows)


## 🚀 Features / Özellikler
* **[EN] Dynamic UI:** Interface automatically adapts based on the selected product category (Tech vs. Clothing).
* **[EN] Auto-Database Generation:** Utilizes Entity Framework Core to automatically generate and manage local SQLite `.db` files.
* **[EN] Defensive Programming:** Input validation and robust error handling to prevent crashes.
* **[TR] Dinamik Arayüz:** Seçilen ürün kategorisine (Teknoloji veya Giyim) göre kendini anında güncelleyen akıllı formlar.
* **[TR] Otomatik Veritabanı:** Entity Framework Core sayesinde ilk açılışta kendi yerel SQLite tablolarını otomatik oluşturur.
* **[TR] Savunmacı Programlama:** Hatalı veri girişlerini (harf yerine sayı vb.) engelleyen güvenli altyapı.

## 🏗️ Software Architecture & Patterns / Yazılım Mimarisi
This project is built to demonstrate core software engineering principles:
* **Layered Architecture:** Clear separation between UI logic (`MainWindow.xaml.cs`) and Business/Data Access logic (`ProductManager.cs`).
* **Object-Oriented Programming (OOP):** Deep utilization of Abstraction, Inheritance, and Polymorphism.
* **Table-Per-Hierarchy (TPH):** Advanced EF Core implementation mapping derived classes (`TechProduct`, `ClothingProduct`) to a single database table using a `Discriminator` column.

## 🛠️ How to Run / Nasıl Çalıştırılır?
1. Clone the repository. (Projeyi klonlayın.)
2. Open the solution in Visual Studio. (Visual Studio'da açın.)
3. Set `UniversalInventoryManager.UI` as the Startup Project. (UI projesini başlangıç projesi yapın.)
4. Press F5. The database will be created automatically on the first run! (F5'e basın. Veritabanı ilk açılışta otomatik oluşacaktır!)

---
**👨‍💻 Author:** Hakan Gürsoy | Software Engineering Student
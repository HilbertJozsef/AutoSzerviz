# Projekt célja és leírása

Az alkalmazás egy autószerviz online időpontfoglaló rendszerét valósítja meg. 
A felületen a látogatók regisztráció és bejelentkezés után rögzíthetik saját járműveiket, böngészhetik a szerviz által kínált szolgáltatásokat és azok árait, 
valamint időpontot foglalhatnak a kiválasztott autóikra.

## Alkalmazott technológiák

- **Keretrendszer:** ASP.NET Core MVC *(.NET 10.0)*

- **Adatbázis-kezelés:** Entity Framework Core *(SQLite)*

- **Autentikáció & Jogosultságkezelés:** ASP.NET Core Identity

- **Frontend:** Razor Views, Bootstrap 5, HTML5, CSS3

 ## Adatbázis-modell *(Entitások)*

- **Service** *(Szolgáltatás)*: Megnevezés, ár *(Ft)*, időtartam *(perc)*.

- **Vehicle** *(Jármű)*: Rendszám, gyártmány/típus, évjárat, tulajdonos *(UserId)*.

- **Appointment** *(Időpontfoglalás)*: Foglalás dátuma és ideje, státusz, kapcsolódó jármű *(VehicleId)*, szolgáltatás *(ServiceId)* és felhasználó *(UserId)*.

## Főbb funkciók és üzleti folyamat

**Felhasználókezelés:** Regisztráció, bejelentkezés, kijelentkezés az Identity modul segítségével.

**Járműkezelés (CRUD):** A bejelentkezett felhasználó rögzítheti, megtekintheti és törölheti saját autóit.

**Szolgáltatáskezelés:** A szerviz szolgáltatásainak és árainak listázása, új szolgáltatások felvétele és törlése.

**Időpontfoglalási folyamat:** A felhasználó kiválasztja a saját autóját, a kívánt szervizszolgáltatást, valamint az időpontot, majd véglegesíti a foglalást.

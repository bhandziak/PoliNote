# 🚀 PoliNote - Instrukcja Uruchomienia Backend (ASP.NET Core)

Poniżej znajduje się instrukcja krok po kroku, jak przygotować środowisko bazodanowe i uruchomić serwer API.

### 0. Czyszczenie środowiska (opcjonalne)
Jeśli została zmieniona struktura db - usuń kontener.

```bash
docker-compose down -v
```

### 1. Uruchomienie bazy danych

```bash
docker-compose up -d
```

### 2. Aktualizacja migracji i bazy danych 
(tylko pierwszy raz po pobraniu zmian z gita)

```bash
dotnet ef database update
```

### 3. Uruchomienie aplikacji .NET na HTTP


### ⚠️ UWAGI
Podczas łączenia aplikacji mobilnej z backendem pamiętaj o dwóch kluczowych kwestiach:

1. Konfiguracja CORS: Backend musi akceptować żądania z portu, na którym działa Twoja aplikacja MAUI (szczególnie w wersji Windows/Catalyst)

2. Autoryzacja (Ciasteczka): Pamiętaj, że aplikacja używa Cookie Authentication. Twój klient HTTP w MAUI musi obsługiwać CookieContainer, aby zachować sesję po zalogowaniu.

3. Adres IP (Emulator Android): Jeśli używasz emulatora Androida, ```localhost``` odnosi się do samego telefonu. Aby połączyć się z komputerem, użyj adresu z kompa.

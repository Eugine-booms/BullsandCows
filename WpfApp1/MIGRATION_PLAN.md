# План миграции BullsAndCowsWPF с .NET 5 на .NET 8

## Контекст

- **Текущий TFM**: `net5.0-windows`
- **Целевой TFM**: `net8.0-windows`
- **Причина**: .NET 5 out-of-support с мая 2022, .NET 8 — LTS до ноября 2026
- **Риски**: WPF на .NET 8/9 работает только на Windows (не кроссплатформенный), но это приемлемо

---

## Шаг 1: Обновление TargetFramework

**Файл**: `WpfApp1/BullsAndCowsWPF.csproj`

Изменить:
```xml
<TargetFramework>net5.0-windows</TargetFramework>
```
на:
```xml
<TargetFramework>net8.0-windows</TargetFramework>
```

> Если нужен .NET 9 (STS): `net9.0-windows`

**Проверка**: Убедиться, что установлен .NET 8 SDK:
```bash
dotnet --list-sdks
```

---

## Шаг 2: Обновление пакетов NuGet

**Файл**: `WpfApp1/BullsAndCowsWPF.csproj`

Текущая версия:
```xml
<PackageReference Include="Microsoft.Extensions.Hosting" Version="6.0.1" />
```

Обновить до совместимой с .NET 8:
```xml
<PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.1" />
```

> Для .NET 9: `Version="9.0.0"`

**Дополнительно** (опционально, рекомендуется):
```xml
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.1" />
```

---

## Шаг 3: Обновление Project SDK

**Файл**: `WpfApp1/BullsAndCowsWPF.csproj`

Текущий SDK:
```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
```

На .NET 8+ используется обычный SDK (WindowsDesktop deprecated):
```xml
<Project Sdk="Microsoft.NET.Sdk">
```

При этом `UseWPF` остаётся:
```xml
<UseWPF>true</UseWPF>
```

---

## Шаг 4: Исправление breaking changes (код C#)

### 4.1 `Random` — thread safety

Текущий код в `RandomNumberGenerator.cs`:
```csharp
private static readonly Random _random = new Random();
```

На .NET 6+ рекомендуется `Random.Shared` (thread-safe, singleton):
```csharp
private static readonly Random _random = Random.Shared; // .NET 6+
```

Или оставить `new Random()` — это не сломается, но `Random.Shared` предпочтительнее.

### 4.2 `ArgumentNullException` — throw helper

На .NET 8 можно использовать `ArgumentNullException.ThrowIfNull`:
```csharp
ArgumentNullException.ThrowIfNull(numberGenerator);
// вместо:
// _numberGenerator = numberGenerator ?? throw new ArgumentNullException(nameof(numberGenerator));
```

> Это опциональная микро-оптимизация, не критична.

### 4.3 `IHost` — `ConfigureServices` signature

Проверить, что `App.ConfigureServices` соответствует новой сигнатуре. На .NET 8 сигнатура обычно та же, но проверить нужно:

```csharp
internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
```

Если есть проблемы с `HostBuilderContext` — заменить на:
```csharp
.ConfigureServices((context, services) => App.ConfigureServices(context, services))
```

### 4.4 `Application.Shutdown` — уже есть в `CloseAppCommand`

Проверить, что `Application.Current.Shutdown()` работает корректно на .NET 8. Должно работать без изменений.

---

## Шаг 5: Обновление XAML / AssemblyInfo

### 5.1 `AssemblyInfo.cs`

Текущий файл `AssemblyInfo.cs` использует `ThemeInfo`. На .NET 8+ это работает, но если появятся ошибки компиляции — убедиться, что файл подключён:

```csharp
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly)]
```

### 5.2 `Properties/Resources.resx`

Файл `Resources.Designer.cs` генерирован для .NET Framework. На .NET 8+ может потребоваться перегенерация. Если сборка ломается:

1. Удалить `Properties/Resources.Designer.cs` и `Properties/Resources.resx`
2. Создать заново через Visual Studio: ПКМ на проект → Properties → Resources

Или добавить в `.csproj`:
```xml
<PropertyGroup>
  <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
</PropertyGroup>
```

> Осторожно: это отключит автоматическую генерацию `AssemblyInfo`. Лучше оставить `GenerateAssemblyInfo` = true и убедиться, что `AssemblyInfo.cs` не конфликтует.

---

## Шаг 6: Проверка сборки и исправление ошибок

```bash
cd "D:\C#\bc\BullsandCows"
dotnet build BullsAndCowsWPF.sln
```

**Типичные ошибки миграции:**

| Ошибка | Решение |
|--------|---------|
| `NETSDK1138` — `Microsoft.NET.Sdk.WindowsDesktop` deprecated | Заменить на `Microsoft.NET.Sdk` |
| `CS0618` — `Random` конструктор без параметров | Использовать `Random.Shared` |
| `CS0121` — конфликт `AssemblyInfo` | Удалить `Properties/AssemblyInfo.cs` или отключить генерацию |
| `Missing method` из `Microsoft.Extensions.Hosting` | Обновить пакет до версии 8.x |

---

## Шаг 7: Runtime-конфигурация (опционально)

**Файл**: `WpfApp1/appsettings.json` (пустой, можно расширить)

Добавить логирование при желании:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

Подключить `Microsoft.Extensions.Logging` в DI если нужно.

---

## Шаг 8: Публикация / доставка

После успешной сборки:

```bash
dotnet publish -c Release -r win-x64 --self-contained false
```

> `--self-contained false` — требует установленный .NET 8 Runtime на машине пользователя
> `--self-contained true` — включает runtime в папку (~150 MB)

---

## Чек-лист для разработчика

- [ ] Шаг 1: TFM → `net8.0-windows` (или `net9.0-windows`)
- [ ] Шаг 2: NuGet пакеты → версии 8.x
- [ ] Шаг 3: SDK → `Microsoft.NET.Sdk`
- [ ] Шаг 4: Исправить `Random` → `Random.Shared` (опционально)
- [ ] Шаг 5: Проверить `AssemblyInfo` / `Resources`
- [ ] Шаг 6: `dotnet build` → исправить ошибки
- [ ] Шаг 7: Проверить `appsettings.json`
- [ ] Шаг 8: `dotnet publish` и тест запуска
- [ ] Git commit с сообщением `chore: migrate from .NET 5 to .NET 8`

---

## Ожидаемый результат `.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
    <ApplicationIcon>Cow_33885.ico</ApplicationIcon>
    <StartupObject>BullsAndCowsWPF.Programm</StartupObject>
  </PropertyGroup>
  <!-- ... ресурсы ... -->
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.1" />
  </ItemGroup>
</Project>
```

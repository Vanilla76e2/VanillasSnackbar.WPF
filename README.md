# VanillasSnackbar.WPF

Кастомный **Snackbar для WPF** с поддержкой режимов **Single** и **Multiple**.  

Изначально разработан для приложения **MigApp**, но может использоваться в любых WPF-проектах.

---

## ✨ Возможности

- Режим **Single** — поочерёдное отображение сообщений, каждое следующее ждёт окончания предыдущего.  
- Режим **Multiple** — несколько сообщений одновременно, новые появляются сверху.  
- Поддержка типов сообщений:
  - Info ℹ️  
  - Success ✅  
  - Warning ⚠️  
  - Error ❌  
- Автоматическое скрытие сообщений через заданное время.  
- Кнопка закрытия для каждого сообщения.  
- Анимация индикатора времени.  

---

## 🚀 Установка

Пока доступна только установка через исходники.

1. Клонируйте репозиторий:
```bash
git clone https://github.com/YourUsername/VanillasSnackbar.WPF.git
```
1. Добавьте проект VanillasSnackbar.WPF в своё решение.
2. Подключите пространство имён в XAML:
```xml
xmlns:snackbar="clr-namespace:VanillasSnackbar.WPF.Controls;assembly=VanillasSnackbar.WPF"
```

---

## 🛠 Использование

### Инициализация менеджера
```csharp
// Multiple режим (по умолчанию)
var snackbarManager = new SnackbarManager(SnackbarMode.Multiple);

// Или Single режим
var snackbarManager = new SnackbarManager(SnackbarMode.Single);
```

### Отображение сообщений
```csharp
snackbarManager.ShowMessage(new SnackbarMessage("Файл успешно сохранен!", SnackbarType.Success, TimeSpan.FromSeconds(3)));
```

### Привязка в XAML
```xml
<ItemsControl ItemsSource="{Binding Snackbar.Messages}">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <StackPanel VerticalAlignment="Bottom"/>
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>
</ItemsControl>
```

---

## 📸 Скриншоты

**Режим Multiple**

<img src="https://media2.giphy.com/media/v1.Y2lkPTc5MGI3NjExcHh6bGd0dGkwbmtubnlqMDc0a3M0NHhwZWp4N3FkMGg0MTFjbTc0YiZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/oXGtmVNEZrVNGVCwEb/giphy.gif" alt="Vanillas-Snackbar-Multiple-Animation" border="0">

**Режим Single**

<img src="https://media4.giphy.com/media/v1.Y2lkPTc5MGI3NjExNjB6bnMxdDJ3YTc2d3JrZGRrc2x2OWVmdjJrd2ptbThxYWduejQ3dyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/dFRR9cv51lA8NZmJ3K/giphy.gif" alt="Vanillas-Snackbar-Multiple-Animation" border="0">

---

## 📄 Лицензия

Этот проект распространяется под лицензией [MIT](LICENSE).  
Вы можете свободно использовать, изменять и распространять эту библиотеку как в личных, так и в коммерческих проектах.

# WebApplication2

![fg](/images/1.png)

![fg](/images/3.png)

![fg](/images/2.png)

### Границы

- Считает не совсем точно, нужно использовать decimal вместо double, а также последний платеж делать чуть меньше остальных
- Есть проблема с локализацией jQuery (валидация десятичных чисел https://github.com/dotnet/AspNetCore.Docs/issues/4076#issue-252296395). Использовал костыльное решение - указал локализацию "en-US"

### Описание:

Веб-приложение состоит из двух страниц.

Страница 1 содержит форму для входных данных:
- поле "Сумма займа";
- поле "Срок займа" (в месяцах);
- поле "Ставка" (в год);
- кнопка "Рассчитать".


Поля формы должны проверяться на корректность введенных значений как на клиентской, так и на серверной стороне (для этого можно воспользоваться атрибутами валидации ASP.NET Core MVC).
Период платежей считается равным одному месяцу.


Страница 2 – результаты расчета:
Должен выводится график платежей в виде таблицы со следующими столбцами:
- "Номер платежа";
- "Дата платежа"; 
- "Размер платежа по телу"; 
- "Размер платежа по %"; 
- "Остаток основного долга".


Под таблицей должно также отображаться итоговое значение всех переплат по кредиту.


Все расчеты должны производиться на серверной стороне.

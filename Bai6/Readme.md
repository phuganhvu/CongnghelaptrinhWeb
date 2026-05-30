# Bài tập ASP.NET MVC - Book Management

## 1. Mục tiêu

Xây dựng ứng dụng quản lý sách đơn giản bằng ASP.NET MVC, áp dụng các kiến thức:

* MVC Architecture
* Routing
* Controller
* View
* Model Binding
* Form Submit (GET/POST)
* Validation (Data Annotation, ModelState)

---

## 2. Cấu trúc chương trình

### Model

File: `Models/Book.cs`

Chứa thông tin của một cuốn sách:

* Id
* Name
* Price

Ngoài ra sử dụng Data Annotation để kiểm tra dữ liệu:

```csharp
[Required(ErrorMessage = "Không được để trống")]
public string Name { get; set; }

[Range(1, double.MaxValue,
ErrorMessage = "Giá phải lớn hơn 0")]
public double Price { get; set; }
```

---

### Controller

File: `Controllers/BookController.cs`

Controller chịu trách nhiệm xử lý yêu cầu từ người dùng.

#### Action Index()

Hiển thị danh sách sách.

```csharp
public IActionResult Index()
{
    return View(books);
}
```

---

#### Action Detail(int id)

Hiển thị thông tin chi tiết của một cuốn sách theo Id.

```csharp
public IActionResult Detail(int id)
{
    var book = books.FirstOrDefault(b => b.Id == id);
    return View(book);
}
```

---

#### Action Create() - GET

Hiển thị form thêm sách.

```csharp
[HttpGet]
public IActionResult Create()
{
    return View();
}
```

---

#### Action Create(Book book) - POST

Nhận dữ liệu từ form.

Kiểm tra dữ liệu bằng ModelState.

Nếu hợp lệ:

* Thêm sách vào danh sách
* Chuyển về trang danh sách sách

```csharp
[HttpPost]
public IActionResult Create(Book book)
{
    if (ModelState.IsValid)
    {
        books.Add(book);
        return RedirectToAction("Index");
    }

    return View(book);
}
```

---

### View

#### Index.cshtml

Hiển thị bảng danh sách sách.

Người dùng có thể:

* Xem chi tiết sách
* Thêm sách mới

---

#### Detail.cshtml

Hiển thị:

* Id
* Tên sách
* Giá sách

---

#### Create.cshtml

Hiển thị form nhập:

* Tên sách
* Giá sách

Khi nhập sai dữ liệu sẽ hiển thị thông báo lỗi.

---

## 3. Luồng hoạt động của chương trình

Bước 1:

Người dùng truy cập:

```
/Book/Index
```

Hệ thống hiển thị danh sách sách.

↓

Bước 2:

Người dùng nhấn:

```
Thêm Sách Mới
```

↓

Bước 3:

Controller gọi:

```csharp
Create() [HttpGet]
```

để hiển thị form nhập liệu.

↓

Bước 4:

Người dùng nhập thông tin sách và nhấn Submit.

↓

Bước 5:

Controller gọi:

```csharp
Create(Book book) [HttpPost]
```

ASP.NET MVC sử dụng Model Binding để tự động gán dữ liệu từ form vào đối tượng Book.

↓

Bước 6:

ModelState kiểm tra dữ liệu:

* Tên sách không được rỗng
* Giá phải lớn hơn 0

↓

Bước 7:

Nếu dữ liệu hợp lệ:

* Thêm sách vào danh sách
* Chuyển về trang Index

Nếu dữ liệu không hợp lệ:

* Hiển thị lỗi trên form

↓

Bước 8:

Danh sách sách được cập nhật và hiển thị cho người dùng.

---

## 4. Kết quả đạt được

* Hiển thị danh sách sách
* Xem chi tiết sách
* Thêm sách mới
* Validation dữ liệu đầu vào
* Sử dụng MVC đầy đủ
* Áp dụng GET/POST và Model Binding

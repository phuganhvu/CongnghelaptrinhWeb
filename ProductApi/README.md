Video demo
https://drive.google.com/drive/folders/19rU6P3fUkLWZR1No84kxiN-5SFYRFVNl?zarsrc=410&direction=a

# Hướng dẫn kết nối và kiểm thử API Quản lý Sản phẩm (Product API)

Dự án này là một RESTful API được xây dựng bằng **ASP.NET Core Web API (NET 8.0)** nhằm quản lý sản phẩm với các chức năng thêm mới sản phẩm và lấy thông tin sản phẩm theo ID kèm theo ràng buộc validation dữ liệu đầu vào.

---

## 1. Cấu hình & Chạy Project local

Để chạy ứng dụng ở máy local, bạn cần cài đặt **.NET 8.0 SDK**.

Chạy lệnh sau tại thư mục gốc của project:
```bash
dotnet run
```
Sau khi chạy thành công, ứng dụng sẽ chạy tại URL mặc định:
- **HTTPS:** `https://localhost:7258`
- **HTTP:** `http://localhost:5219`
- **Swagger UI (tài liệu API trực quan):** `https://localhost:7258/swagger/index.html` hoặc `http://localhost:5219/swagger/index.html`

---

## 2. Các API Endpoint & Kiểm thử qua Postman

### 2.1. API Thêm Mới Sản Phẩm (POST)
- **Endpoint:** `POST /api/products`
- **Địa chỉ:** `http://localhost:5219/api/products` hoặc `https://localhost:7258/api/products`
- **Headers:** `Content-Type: application/json`

#### Trường hợp 1: Dữ liệu hợp lệ (Thêm thành công)
- **Body (JSON):**
  ```json
  {
    "name": "Bàn phím cơ",
    "price": 1250000
  }
  ```
- **Kết quả trả về (200 OK):**
  ```json
  {
    "id": 1,
    "name": "Bàn phím cơ",
    "price": 1250000.00
  }
  ```

#### Trường hợp 2: Lỗi validation (Ví dụ: tên quá ngắn, giá <= 0)
- **Body (JSON):**
  ```json
  {
    "name": "Lo",
    "price": -5000
  }
  ```
- **Kết quả trả về (400 Bad Request):**
  ```json
  [
    "Name phải có ít nhất 3 ký tự",
    "Price phải lớn hơn 0"
  ]
  ```

---

### 2.2. API Lấy Thông Tin Sản Phẩm Theo ID (GET)
- **Endpoint:** `GET /api/products/{id}`
- **Địa chỉ:** `http://localhost:5219/api/products/1` hoặc `https://localhost:7258/api/products/1`

#### Trường hợp 1: Tìm thấy sản phẩm (ID hợp lệ)
- **Kết quả trả về (200 OK):**
  ```json
  {
    "id": 1,
    "name": "Bàn phím cơ",
    "price": 1250000.00
  }
  ```

#### Trường hợp 2: Lỗi validate ID (Không phải số nguyên dương, e.g. ID = 0 hoặc âm)
- **Yêu cầu:** `GET /api/products/0`
- **Kết quả trả về (400 Bad Request):**
  ```json
  {
    "message": "Id phải là số nguyên dương"
  }
  ```

#### Trường hợp 3: Không tìm thấy sản phẩm (ID không tồn tại)
- **Yêu cầu:** `GET /api/products/999`
- **Kết quả trả về (404 Not Found):**
  ```json
  {
    "message": "Không tìm thấy sản phẩm"
  }
  ```

---

## 3. Hướng dẫn Front-End (Web & Mobile) Kết Nối API

### 3.1. Hướng dẫn dành cho Web (JavaScript / TypeScript / React / Vue)

Sử dụng thư viện `axios` hoặc API `fetch` có sẵn của trình duyệt:

```javascript
const API_URL = "http://localhost:5219/api/products";

// 1. Gửi yêu cầu thêm mới sản phẩm
async function createProduct(name, price) {
  try {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ name, price }),
    });

    if (!response.ok) {
      // Nhận danh sách lỗi validate từ backend
      const errors = await response.json();
      console.error("Lỗi validate sản phẩm:", errors);
      alert("Lỗi validate: " + JSON.stringify(errors));
      return;
    }

    const data = await response.json();
    console.log("Thêm sản phẩm thành công:", data);
  } catch (error) {
    console.error("Lỗi hệ thống hoặc kết nối:", error);
  }
}

// 2. Gửi yêu cầu lấy chi tiết sản phẩm theo ID
async function getProductById(id) {
  try {
    const response = await fetch(`${API_URL}/${id}`);
    
    if (!response.ok) {
      const errorData = await response.json();
      console.warn("Lỗi:", errorData.message || response.statusText);
      return null;
    }

    const product = await response.json();
    console.log("Chi tiết sản phẩm:", product);
    return product;
  } catch (error) {
    console.error("Lỗi kết nối:", error);
  }
}
```

> [!IMPORTANT]
> **CORS Policy (Cross-Origin Resource Sharing):**
> Nếu Front-End chạy ở một domain khác (ví dụ: `http://localhost:3000`), bạn cần cấu hình CORS ở file `Program.cs` của Backend để cho phép Front-End gọi API. Bạn có thể thêm đoạn code sau vào Backend:
> ```csharp
> builder.Services.AddCors(options => {
>     options.AddDefaultPolicy(policy => {
>         policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
>     });
> });
> // ...
> app.UseCors();
> ```

---

### 3.2. Hướng dẫn dành cho Mobile (React Native / Flutter)

#### Đối với React Native (Fetch / Axios)
Tương tự như trình duyệt Web, nhưng khi kiểm thử trên máy ảo (Emulator/Simulator) hoặc thiết bị thật, bạn **không được dùng `localhost`** vì thiết bị ảo coi `localhost` là chính nó.
- **Trực tiếp Android Emulator:** Thay `localhost` bằng `10.0.2.2` (ví dụ: `http://10.0.2.2:5219/api/products`).
- **iOS Simulator:** Có thể dùng `localhost` hoặc IP cục bộ của máy tính của bạn.
- **Thiết bị thật:** Sử dụng IP cục bộ của máy tính phát Wi-Fi (ví dụ: `http://192.168.1.100:5219/api/products`).

#### Đối với Flutter (sử dụng thư viện HTTP hoặc Dio)
```dart
import 'dart:convert';
import 'package:http/http.dart' as http;

// Thay thế URL bằng IP phù hợp (e.g. 10.0.2.2 cho Android Emulator)
final String apiUrl = 'http://10.0.2.2:5219/api/products';

Future<void> createProduct(String name, double price) async {
  try {
    final response = await http.post(
      Uri.parse(apiUrl),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, dynamic>{
        'name': name,
        'price': price,
      }),
    );

    if (response.statusCode == 200) {
      final product = jsonDecode(response.body);
      print('Thêm sản phẩm thành công: $product');
    } else {
      // Trả về danh sách lỗi validate từ Backend
      final errors = jsonDecode(response.body);
      print('Lỗi validate: $errors');
    }
  } catch (e) {
    print('Lỗi kết nối: $e');
  }
}
```

1. Middleware trong ASP.NET Core dùng để làm gì?

Xử lý request/response trước hoặc sau khi đến Controller.

2. Middleware khác Controller ở điểm nào?
Middleware: Xử lý request trong pipeline, có thể chặn request.
Controller: Xử lý nghiệp vụ và trả kết quả cho người dùng.
3. Ý nghĩa của dòng lệnh:
await _next(context);

Chuyển request sang middleware tiếp theo trong pipeline.

4. Vì sao return; thì request không đi tiếp vào Controller?

Vì pipeline bị dừng tại middleware hiện tại, không gọi middleware tiếp theo.

5. Nếu đặt middleware sau app.MapControllerRoute(...) thì sao?

Middleware có thể không được chạy trước Controller, dẫn đến không kiểm tra hoặc chặn được request như mong muốn.

6. Muốn thêm middleware khác thì làm thế nào?
app.UseMiddleware<MiddlewareA>();
app.UseMiddleware<MiddlewareB>();

Các middleware sẽ chạy theo thứ tự khai báo.
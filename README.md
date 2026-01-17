# School Management API - Layered Architecture
Load terimal; cd d:\Git\btapfront\WebAPI; dotnet run --urls="http://localhost:5000" hoặc cd d:\Git\btapfront\WebAPI
dotnet run
## Giới thiệu
Project ASP.NET Core Web API được xây dựng theo mô hình Layered Architecture (Clean Architecture rút gọn) với 4 layer:
- **Domain**: Chứa các entity models
- **Infrastructure**: Chứa DbContext, Repository Pattern
- **Application**: Chứa Service Pattern
- **WebAPI**: Chứa Controllers và cấu hình

## Cấu trúc Project

```
btapfront/
├── Domain/
│   ├── Entities/
│   │   ├── Class.cs
│   │   └── Student.cs
│   └── Domain.csproj
├── Infrastructure/
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── Repositories/
│   │   ├── IClassRepository.cs
│   │   ├── ClassRepository.cs
│   │   ├── IStudentRepository.cs
│   │   └── StudentRepository.cs
│   └── Infrastructure.csproj
├── Application/
│   ├── Services/
│   │   ├── IClassService.cs
│   │   ├── ClassService.cs
│   │   ├── IStudentService.cs
│   │   └── StudentService.cs
│   └── Application.csproj
├── WebAPI/
│   ├── Controllers/
│   │   ├── ClassesController.cs
│   │   └── StudentsController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── WebAPI.csproj
└── SchoolManagementAPI.postman_collection.json
```

## Yêu cầu hệ thống
- .NET 8.0 SDK
- Visual Studio 2022 hoặc VS Code
- Postman (để test API)

## Cách chạy project

### 1. Restore dependencies
```bash
cd WebAPI
dotnet restore
```

### 2. Build project
```bash
dotnet build
```

### 3. Run project
```bash
dotnet run
```

Ứng dụng sẽ chạy tại:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `http://localhost:5000` hoặc `https://localhost:5001`

## API Endpoints

### Classes API
| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/classes` | Lấy danh sách tất cả lớp |
| GET | `/api/classes/{id}` | Lấy chi tiết lớp theo Id |
| POST | `/api/classes` | Tạo lớp mới |

### Students API
| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/students` | Lấy danh sách tất cả sinh viên |
| GET | `/api/students/{id}` | Lấy chi tiết sinh viên theo Id |
| POST | `/api/students` | Thêm sinh viên mới |
| PUT | `/api/students/{id}` | Cập nhật thông tin sinh viên |
| DELETE | `/api/students/{id}` | Xóa sinh viên |

## Hướng dẫn test API bằng Postman

### Bước 1: Import Postman Collection
1. Mở Postman
2. Click **Import**
3. Chọn file `SchoolManagementAPI.postman_collection.json`
4. Collection sẽ được import với tất cả các request

### Bước 2: Thiết lập biến môi trường
Collection đã có sẵn biến `baseUrl` = `http://localhost:5000`

Nếu API chạy trên port khác, cập nhật biến này:
1. Click vào Collection
2. Tab **Variables**
3. Sửa giá trị `baseUrl`

### Bước 3: Test từng API

#### **Test 1: Lấy danh sách lớp (GET All Classes)**
1. Chọn request **Get All Classes**
2. Click **Send**
3. Kết quả mong đợi: Status 200, trả về 2 lớp seed data

```json
[
  {
    "id": 1,
    "name": "Lớp 10A1",
    "description": "Lớp 10 chuyên Toán",
    "students": [...]
  },
  {
    "id": 2,
    "name": "Lớp 10A2",
    "description": "Lớp 10 chuyên Lý",
    "students": [...]
  }
]
```

#### **Test 2: Lấy chi tiết lớp (GET Class By Id)**
1. Chọn request **Get Class By Id**
2. URL: `http://localhost:5000/api/classes/1`
3. Click **Send**
4. Kết quả: Status 200, trả về lớp có Id = 1

#### **Test 3: Tạo lớp mới (POST Create Class)**
1. Chọn request **Create Class**
2. Body đã có sẵn:
```json
{
  "name": "Lớp 10A3",
  "description": "Lớp 10 chuyên Hóa"
}
```
3. Click **Send**
4. Kết quả: Status 201 Created, trả về lớp vừa tạo với Id mới

#### **Test 4: Lấy danh sách sinh viên (GET All Students)**
1. Chọn request **Get All Students**
2. Click **Send**
3. Kết quả: Status 200, trả về 3 sinh viên seed data

```json
[
  {
    "id": 1,
    "fullName": "Nguyễn Văn A",
    "email": "nva@example.com",
    "dateOfBirth": "2008-05-15T00:00:00",
    "classId": 1,
    "class": {...}
  },
  ...
]
```

#### **Test 5: Lấy chi tiết sinh viên (GET Student By Id)**
1. Chọn request **Get Student By Id**
2. URL: `http://localhost:5000/api/students/1`
3. Click **Send**
4. Kết quả: Status 200, trả về sinh viên có Id = 1

#### **Test 6: Thêm sinh viên mới (POST Create Student)**
1. Chọn request **Create Student**
2. Body đã có sẵn:
```json
{
  "fullName": "Phạm Thị D",
  "email": "ptd@example.com",
  "dateOfBirth": "2008-07-25T00:00:00",
  "classId": 1
}
```
3. Click **Send**
4. Kết quả: Status 201 Created, trả về sinh viên vừa tạo với Id mới (Id = 4)

#### **Test 7: Cập nhật sinh viên (PUT Update Student)**
1. Chọn request **Update Student**
2. URL: `http://localhost:5000/api/students/1`
3. Body đã có sẵn:
```json
{
  "id": 1,
  "fullName": "Nguyễn Văn A - Updated",
  "email": "nva.updated@example.com",
  "dateOfBirth": "2008-05-15T00:00:00",
  "classId": 2
}
```
4. Click **Send**
5. Kết quả: Status 200, trả về sinh viên đã được cập nhật

**Lưu ý quan trọng**: 
- Id trong body phải khớp với Id trong URL
- Phải có đầy đủ các trường bắt buộc

#### **Test 8: Xóa sinh viên (DELETE Student)**
1. Chọn request **Delete Student**
2. URL: `http://localhost:5000/api/students/3`
3. Click **Send**
4. Kết quả: Status 200, message "Xóa sinh viên thành công"

#### **Test 9: Test lỗi - Get Student không tồn tại**
1. Chọn request **Get Student By Id**
2. Sửa URL thành: `http://localhost:5000/api/students/999`
3. Click **Send**
4. Kết quả: Status 404 Not Found
```json
{
  "message": "Không tìm thấy sinh viên với Id = 999"
}
```

### Bước 4: Test quy trình CRUD đầy đủ

**Scenario: Quản lý sinh viên từ A đến Z**

1. **Xem danh sách ban đầu**
   - GET `/api/students` → Có 3 sinh viên

2. **Thêm sinh viên mới**
   - POST `/api/students` với data:
   ```json
   {
     "fullName": "Hoàng Văn E",
     "email": "hve@example.com",
     "dateOfBirth": "2008-09-10T00:00:00",
     "classId": 2
   }
   ```
   - Lưu lại Id trả về (ví dụ: Id = 4)

3. **Xem chi tiết sinh viên vừa tạo**
   - GET `/api/students/4`

4. **Cập nhật thông tin**
   - PUT `/api/students/4` với data:
   ```json
   {
     "id": 4,
     "fullName": "Hoàng Văn E - Đã sửa",
     "email": "hve.updated@example.com",
     "dateOfBirth": "2008-09-10T00:00:00",
     "classId": 1
   }
   ```

5. **Xóa sinh viên**
   - DELETE `/api/students/4`

6. **Xác nhận đã xóa**
   - GET `/api/students/4` → Kết quả: 404 Not Found

## Database Schema

### Bảng Classes
| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | PRIMARY KEY |
| Name | string(100) | NOT NULL |
| Description | string(500) | |

### Bảng Students
| Column | Type | Constraints |
|--------|------|-------------|
| Id | int | PRIMARY KEY |
| FullName | string(100) | NOT NULL |
| Email | string(100) | NOT NULL |
| DateOfBirth | DateTime | NOT NULL |
| ClassId | int | FOREIGN KEY → Classes(Id) |

**Relationship**: One-to-Many (1 Class có nhiều Students)

## Seed Data

Khi chạy ứng dụng, database sẽ tự động có dữ liệu mẫu:

**Classes:**
- Id: 1, Name: "Lớp 10A1", Description: "Lớp 10 chuyên Toán"
- Id: 2, Name: "Lớp 10A2", Description: "Lớp 10 chuyên Lý"

**Students:**
- Id: 1, FullName: "Nguyễn Văn A", Email: "nva@example.com", ClassId: 1
- Id: 2, FullName: "Trần Thị B", Email: "ttb@example.com", ClassId: 1
- Id: 3, FullName: "Lê Văn C", Email: "lvc@example.com", ClassId: 2

## Kiến trúc & Dependency Flow

```
WebAPI (Controllers)
    ↓ chỉ gọi
Application (Services)
    ↓ chỉ gọi
Infrastructure (Repositories)
    ↓ chỉ gọi
Infrastructure (DbContext + InMemory DB)
    ↓ sử dụng
Domain (Entities)
```

**Nguyên tắc:**
- ✅ Controller → Service
- ✅ Service → Repository
- ✅ Repository → DbContext
- ❌ Controller KHÔNG được gọi DbContext trực tiếp
- ❌ Controller KHÔNG được gọi Repository trực tiếp

## Công nghệ sử dụng
- **ASP.NET Core 8.0 Web API**
- **Entity Framework Core 8.0 InMemory**
- **Dependency Injection**
- **Repository Pattern**
- **Service Pattern**
- **Swagger/OpenAPI**

## Lưu ý quan trọng
1. **InMemory Database**: Dữ liệu sẽ mất khi restart ứng dụng
2. **Không cần Migration**: InMemory DB tự động tạo schema
3. **Không cần SQL Server**: Database hoàn toàn trong RAM
4. **Swagger UI**: Có sẵn khi chạy app, truy cập tại root URL

## Troubleshooting

### Lỗi: Port đã được sử dụng
```bash
# Đổi port trong Program.cs hoặc chạy với port khác:
dotnet run --urls="http://localhost:5050"
```

### Lỗi: Không build được
```bash
# Xóa bin/obj và restore lại:
dotnet clean
dotnet restore
dotnet build
```

### Lỗi: Không kết nối được API từ Postman
- Kiểm tra API đang chạy
- Kiểm tra port đúng chưa
- Tắt firewall/antivirus thử
- Dùng `http` thay vì `https` nếu có lỗi SSL

---

**Tác giả**: GitHub Copilot  
**Ngày tạo**: January 17, 2026  
**Phiên bản**: 1.0

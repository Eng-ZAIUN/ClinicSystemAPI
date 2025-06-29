# 🏥 ClinicSystemAPI

✨ **وصف المشروع:**
نظام إدارة عيادة طبية بسيط مبني باستخدام **ASP.NET Core API** مع تطبيق أفضل ممارسات 3 Tire  Architecture:
- 🔹 طبقة API
- 🔹 طبقة BLL (Business Logic Layer)
- 🔹 طبقة DAL (Data Access Layer)

🎯 المشروع يدعم:
- تسجيل الدخول (JWT)
- تسجيل مستخدم جديد
- إدارة المرضى (CRUD)
- إدارة المواعيد (CRUD)
- إدارة الأدوار (Roles)

---

## 🚀 التقنيات المستخدمة

- ✅ ASP.NET Core 8
- ✅ Entity Framework Core
- ✅ JWT Authentication
- ✅ AutoMapper
- ✅ FluentValidation
- ✅ SQL Server
- ✅ 3 Tier Architecture
- ✅ Middlewares
---

## 📂 هيكل المشروع

```bash
ClinicSystemAPI/
│
├── API/        # طبقة Controllers و Endpoints
├── BLL/        # الخدمات والمنطق التجاري
├── DAL/        # الوصول إلى قاعدة البيانات
├── publish/    # ملفات النشر (غير مضمنة في Git)
└── ClinicSystemAPI.sln  # ملف الحل

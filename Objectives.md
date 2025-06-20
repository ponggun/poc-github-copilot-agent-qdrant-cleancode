## **🤖 Task 0: ศึกษาการใช้งาน GitHub Copilot coding agent**
- เปลี่ยนบทบาทจาก Developer เป็น Project Manager
- ให้ Copilot ช่วยในการเขียนโค้ดตามความต้องการที่ระบุ

## **🧩 Task 1: การออกแบบสถาปัตยกรรมระบบตาม Clean Architecture และ Twelve-Factor App**

**1.1 โครงสร้างโปรเจกต์ตาม Clean Architecture**

- **Presentation Layer**: ประกอบด้วย Evaluator, Web API, และ Web App
- **Application Layer**: รวม Business Logic และ Interface ต่างๆ
- **Domain Layer**: ประกอบด้วย Entities และ Interfaces
- **Infrastructure Layer**: จัดการกับการเชื่อมต่อ Qdrant, Embedding Models, และบริการอื่นๆ

**1.2 การปรับใช้หลักการ Twelve-Factor App**

- **Codebase**: ใช้ระบบควบคุมเวอร์ชัน เช่น Git เพื่อจัดการโค้ดเบสเดียวที่ใช้สำหรับหลายการปรับใช้
- **Dependencies**: ประกาศและจัดการ Dependencies อย่างชัดเจนโดยใช้ไฟล์ .csproj และ NuGet Packages
- **Config**: เก็บค่าคอนฟิกที่เปลี่ยนแปลงตามสภาพแวดล้อมไว้ใน Environment Variables หรือไฟล์คอนฟิก เช่น appsettings.Development.json และ appsettings.Production.json
- **Backing Services**: ปฏิบัติต่อบริการภายนอก (เช่น Qdrant, Embedding Models) เป็นทรัพยากรที่แนบมาภายนอกและสามารถเปลี่ยนแปลงได้โดยไม่กระทบกับโค้ดเบส
- **Build, Release, Run**: แยกขั้นตอนการ Build, Release และ Run อย่างชัดเจน
- **Processes**: ออกแบบแอปพลิเคชันให้ทำงานแบบ Stateless โดยไม่เก็บสถานะภายในโปรเซส
- **Port Binding**: แอปพลิเคชันควรเปิดพอร์ตของตัวเองและไม่พึ่งพา Web Server ภายนอก
- **Concurrency**: ออกแบบแอปพลิเคชันให้สามารถปรับขนาดได้โดยการเพิ่มจำนวนโปรเซสหรืออินสแตนซ์
- **Disposability**: ออกแบบให้โปรเซสสามารถเริ่มต้นและปิดได้อย่างรวดเร็วเพื่อรองรับการปรับขนาดและการปรับปรุงระบบ
- **Dev/Prod Parity**: รักษาความสอดคล้องระหว่างสภาพแวดล้อมการพัฒนา การทดสอบ และการผลิต
- **Logs**: แอปพลิเคชันควรเขียนล็อกเป็นสตรีมของเหตุการณ์ไปยัง stdout หรือ stderr
- **Admin Processes**: งานผู้ดูแลระบบ เช่น การย้ายข้อมูลหรือการทำความสะอาดข้อมูล ควรแยกออกจากโปรเซสหลักและสามารถเรียกใช้ได้ตามต้องการ

**1.3 การตั้งค่า Docker Compose**

- สร้างบริการสำหรับ Qdrant (ทั้ง On-Premise และ On-Cloud)
- รวมถึงบริการอื่นๆ ที่จำเป็น เช่น Embedding Models, Web API, และ Web App

## **🧠 Task 2: การเตรียมและจัดการข้อมูล**

**2.1 เตรียม Dataset**

- รวบรวมคำศัพท์และประโยคทั้งภาษาไทยและอังกฤษ
- จัดรูปแบบข้อมูลให้อยู่ในรูปแบบที่เหมาะสมสำหรับการทำ Embedding

**2.2 สร้าง Embedding**

- ใช้โมเดลต่างๆ เช่น OpenAI’s text-embedding-3-small, text-embedding-3-large, และ Ollama’s bge-m3
- แปลงข้อความเป็นเวกเตอร์และจัดเก็บใน Qdrant

**2.3 จัดเก็บข้อมูลใน Qdrant**

- สร้าง Collections สำหรับแต่ละโมเดลและประเภทของข้อมูล
- ตั้งค่า Indexes และ Payloads ตามความเหมาะสม

## **🔍 Task 3: การทดสอบการค้นหา**

**3.1 Similarity Search**

- ทดสอบการค้นหาด้วยเวกเตอร์ที่ได้จาก Embedding
- วัดความแม่นยำและประสิทธิภาพของการค้นหา

**3.2 Full Text Search**

- ใช้ความสามารถ Full-Text Search ของ Qdrant เพื่อค้นหาข้อมูลตามคำหรือวลี
- เปรียบเทียบผลลัพธ์กับ Similarity Search

**3.3 Hybrid Search**

- รวมผลลัพธ์จาก Similarity Search และ Full Text Search
- วิเคราะห์ความแม่นยำและประสิทธิภาพของการค้นหาแบบผสม

## **📊 Task 4: การประเมินผลและการแสดงผล**

**4.1 สร้าง Evaluator Tool**

- พัฒนาเครื่องมือสำหรับวัดผลการค้นหาในแต่ละประเภท
- เก็บข้อมูลการทดสอบและผลลัพธ์ในรูปแบบที่สามารถนำไปวิเคราะห์ต่อได้

**4.2 แสดงผลในรูปแบบกราฟและตาราง**

- ใช้เครื่องมือ Visualization เช่น Chart.js หรือ D3.js เพื่อแสดงผลลัพธ์
- นำเสนอข้อมูลในรูปแบบที่เข้าใจง่ายและเปรียบเทียบได้

## **🧪 Task 5: การทดสอบกับข้อมูลขนาดใหญ่**

**5.1 สร้าง Dataset ขนาด 1 ล้านรายการ**

- จำลองข้อมูลหรือใช้ Dataset ที่มีอยู่แล้วเพื่อทดสอบ
- แปลงข้อมูลเป็นเวกเตอร์และจัดเก็บใน Qdrant

**5.2 ทดสอบประสิทธิภาพการค้นหา**

- วัดเวลาในการค้นหาและความแม่นยำของผลลัพธ์ในแต่ละประเภทการค้นหา
- วิเคราะห์ผลลัพธ์เพื่อปรับปรุงระบบให้มีประสิทธิภาพมากยิ่งขึ้น

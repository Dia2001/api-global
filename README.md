# Api này được tạo ra chỉ để thể hiện hết cấu trúc 1 project font-end nên về phần nghiệp vụ dự án sẽ ko chính xác.

# Information Project
Do nhu cầu phải code quá nhiều loại ngôn ngữ front-end, và làm quen với rất nhiều kiến trúc thư viện fort-end khác nhau. Việc đầu tiên để tiếp cận là xây dựng 1 mini app để hiểu rõ luồng và kiến trúc dự án. Do đó mình có ý tưởng xây dựng 1 api chung để demo các project. 
# Role
Admin có thể có tất các chức năng của hệ thống.
Nhân viên xem sản phẩm, sửa số lượng sản phẩm(Thêm và xóa sản phẩm là do admin nhân viên chỉ được phép sửa số lượng sản phẩm khi nhập hàng).
Customer(user) xem sản phẩm, thêm sản phẩm vào giỏ hàng(thao tác thêm số lượng, giảm số lượng, xóa sản phẩm ra khỏi giỏ hàng), xem sản phẩm ở trong giỏ hàng, xem thông tin đơn vị vận chuyển.  
### Do mini app nên tính năng sẽ giới hạn như sau:
- Customer(user): có thể xem tất cả các sản phẩm --> không cần role, thêm một hoặc nhiều sản phẩm vào giỏ hàng(Khi ta nhấn thêm sản phẩm vào giỏ hàng bên phía front-end thì không cớ lưu vào database mục đích chỉ để áp dụng sate manage ở phía front-end. Ở bên màn hình giỏ hàng nhấn thêm thì sẽ lưu tất cả các sản phẩm ở phần sate management) --> role(user)
- Employee: có thể xem tất cả các sản phẩm --> không cần role, sửa số lượng sản phẩm --> role(employee)
# Database
### Product
![image](https://github.com/Dia2001/api-global/assets/88370983/31ffe3ef-3a32-44ad-ab2a-b6dc938c6ab8)


### Cart
![image](https://github.com/Dia2001/api-global/assets/88370983/165548b5-d218-426d-a686-e73a4994c221)


# Back-End
# Front-End


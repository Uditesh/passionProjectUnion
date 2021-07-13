# passionProject : Union Travels

## Description
Union Travels is logistic company which has emolpoyees, customers, sectors and orders.

## [GitHub](https://github.com/Uditesh/passionProjectUnion)

## Entities
1. Customers - Details of all the customers. The Customer entity has M-1 relationship with order. 
2. Employees - Details of all the employees. The Employee entity has M-1 relationship with sector.
3. Orders - Information about all the orders. The Order entity has M-1 relationship with sector and customer.
4. Sectors - Information about all the sectors. The Sector entity is connecting table between employees and orders. One sector can have Many employees and orders.

## Tasks
- [x] Create 4 tables to manage data (Using migrations)
- [x] Establish Relationships between them (Three 1-M relations)
- [x] CRUD for the three main entitites
- [x] Including the option to add, update and delete employees from the list of employees page
- [x] Including the option to add, update and delete customers from the list of customers page
- [x] Including the option to add, update and delete orders from the list of orders page
- [x] Image upload feature for employee enitity
- [x] Authentication feature on all three enitities
- [x] Responsive design
 
## Future Scopes
- [ ] Implementing client side website view
- [ ] improve design perspective

## Learning Challenge 
* Implementing the 1-M relationship

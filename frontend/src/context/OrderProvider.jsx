import React, { createContext, useState } from 'react';

const OrderContext = createContext();

const OrderProvider = ({ children }) => {
    const [orderData, setOrderData] = useState([
            { id: "ORD001", customer: "John Doe", total: 235.4, status: "Delivered", date: "2023-07-01", products: [{ name: "Milk", quantity: 2, price: 100 }, { name: "Bread", quantity: 1, price: 35.4 }] },
            { id: "ORD002", customer: "Jane Smith", total: 412.0, status: "Processing", date: "2023-07-02", products: [{ name: "Cheese", quantity: 3, price: 150 }, { name: "Butter", quantity: 2, price: 112 }] },
            { id: "ORD003", customer: "Bob Johnson", total: 162.5, status: "Shipped", date: "2023-07-03", products: [{ name: "Eggs", quantity: 12, price: 50 }, { name: "Juice", quantity: 1, price: 112.5 }] },
            { id: "ORD004", customer: "Alice Brown", total: 750.2, status: "Pending", date: "2023-07-04", products: [{ name: "Chicken", quantity: 5, price: 150 }, { name: "Rice", quantity: 10, price: 600.2 }] },
            { id: "ORD005", customer: "Charlie Wilson", total: 95.8, status: "Delivered", date: "2023-07-05", products: [{ name: "Pasta", quantity: 4, price: 80 }, { name: "Sauce", quantity: 1, price: 15.8 }] },
            { id: "ORD006", customer: "Eva Martinez", total: 310.75, status: "Processing", date: "2023-07-06", products: [{ name: "Fish", quantity: 3, price: 200 }, { name: "Lemon", quantity: 5, price: 110.75 }] },
            { id: "ORD007", customer: "David Lee", total: 528.9, status: "Shipped", date: "2023-07-07", products: [{ name: "Steak", quantity: 2, price: 400 }, { name: "Potatoes", quantity: 10, price: 128.9 }] },
            { id: "ORD008", customer: "Grace Taylor", total: 189.6, status: "Delivered", date: "2023-07-08", products: [{ name: "Apples", quantity: 10, price: 100 }, { name: "Bananas", quantity: 8, price: 89.6 }] },
        
    ]);

    const updateOrderStatus = (orderId, newStatus) => {
        setOrderData(prevData =>
            prevData.map(order =>
                order.id === orderId ? { ...order, status: newStatus } : order
            )
        );
    };

    return (
        <OrderContext.Provider value={{ orderData, updateOrderStatus }}>
            {children}
        </OrderContext.Provider>
    );
};

export { OrderContext, OrderProvider };
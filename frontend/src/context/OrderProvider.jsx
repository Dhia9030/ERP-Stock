import React, { useState, createContext, useContext } from 'react';

const orderContext = createContext();

const OrderProvider = ({ children }) => {
  const initialOrderData = [
    {
      id: "ORD001",
      customer: "Achref Hemissi",
      total: 235.4,
      status: "Delivered",
      date: "2023-07-01",
      products: [
        {
          name: "Milk",
          quantity: 2,
          price: 100,
          discount: 0.1,
          details: [
            { productItemId: 1, warehouse: "Main Warehouse", location: "Aisle 1" },
            { productItemId: 2, warehouse: "Main Warehouse", location: "Aisle 1" }
          ]
        },
        {
          name: "Bread",
          quantity: 1,
          price: 35.4,
          discount: 0.05,
          details: [
            { productItemId: 3, warehouse: "Main Warehouse", location: "Aisle 2" }
          ]
        }
      ]
    },
    {
      id: "ORD002",
      customer: "Jane Smith",
      total: 412.0,
      status: "Processing",
      date: "2023-07-02",
      products: [
        {
          name: "Cheese",
          quantity: 3,
          price: 150,
          discount: 0.2,
          details: [
            { productItemId: 4, warehouse: "Secondary Warehouse", location: "Aisle 3" },
            { productItemId: 5, warehouse: "Secondary Warehouse", location: "Aisle 3" },
            { productItemId: 6, warehouse: "Secondary Warehouse", location: "Aisle 3" }
          ]
        },
        {
          name: "Butter",
          quantity: 2,
          price: 112,
          discount: 0.15,
          details: [
            { productItemId: 7, warehouse: "Secondary Warehouse", location: "Aisle 4" },
            { productItemId: 8, warehouse: "Secondary Warehouse", location: "Aisle 4" }
          ]
        }
      ]
    },
    {
      id: "ORD003",
      customer: "Bob Johnson",
      total: 162.5,
      status: "Shipped",
      date: "2023-07-03",
      products: [
        {
          name: "Eggs",
          quantity: 6,
          price: 50,
          discount: 0.1,
          details: [
            { productItemId: 9, warehouse: "Main Warehouse", location: "Aisle 5" },
            { productItemId: 10, warehouse: "Main Warehouse", location: "Aisle 5" },
            { productItemId: 11, warehouse: "Main Warehouse", location: "Aisle 5" },
            { productItemId: 12, warehouse: "Main Warehouse", location: "Aisle 5" },
            { productItemId: 13, warehouse: "Main Warehouse", location: "Aisle 5" },
            { productItemId: 14, warehouse: "Main Warehouse", location: "Aisle 5" }
          ]
        },
        {
          name: "Juice",
          quantity: 1,
          price: 112.5,
          discount: 0.05,
          details: [
            { productItemId: 15, warehouse: "Main Warehouse", location: "Aisle 6" }
          ]
        }
      ]
    },
    {
      id: "ORD004",
      customer: "Alice Brown",
      total: 750.2,
      status: "Pending",
      date: "2023-07-04",
      products: [
        {
          name: "Chicken",
          quantity: 5,
          price: 150,
          discount: 0.25,
          details: [
            { productItemId: 16, warehouse: "Main Warehouse", location: "Aisle 7" },
            { productItemId: 17, warehouse: "Main Warehouse", location: "Aisle 7" },
            { productItemId: 18, warehouse: "Main Warehouse", location: "Aisle 7" },
            { productItemId: 19, warehouse: "Main Warehouse", location: "Aisle 7" },
            { productItemId: 20, warehouse: "Main Warehouse", location: "Aisle 7" }
          ]
        },
        {
          name: "Rice",
          quantity: 6,
          price: 600.2,
          discount: 0.1,
          details: [
            { productItemId: 21, warehouse: "Main Warehouse", location: "Aisle 8" },
            { productItemId: 22, warehouse: "Main Warehouse", location: "Aisle 8" },
            { productItemId: 23, warehouse: "Main Warehouse", location: "Aisle 8" },
            { productItemId: 24, warehouse: "Main Warehouse", location: "Aisle 8" },
            { productItemId: 25, warehouse: "Main Warehouse", location: "Aisle 8" },
            { productItemId: 26, warehouse: "Main Warehouse", location: "Aisle 8" }
          ]
        }
      ]
    },
    {
      id: "ORD005",
      customer: "Charlie Wilson",
      total: 95.8,
      status: "Delivered",
      date: "2023-07-05",
      products: [
        {
          name: "Pasta",
          quantity: 4,
          price: 80,
          discount: 0.05,
          details: [
            { productItemId: 27, warehouse: "Secondary Warehouse", location: "Aisle 9" },
            { productItemId: 28, warehouse: "Secondary Warehouse", location: "Aisle 9" },
            { productItemId: 29, warehouse: "Secondary Warehouse", location: "Aisle 9" },
            { productItemId: 30, warehouse: "Secondary Warehouse", location: "Aisle 9" }
          ]
        },
        {
          name: "Sauce",
          quantity: 1,
          price: 15.8,
          discount: 0.1,
          details: [
            { productItemId: 31, warehouse: "Secondary Warehouse", location: "Aisle 10" }
          ]
        }
      ]
    },
    {
      id: "ORD006",
      customer: "Eva Martinez",
      total: 310.75,
      status: "Processing",
      date: "2023-07-06",
      products: [
        {
          name: "Fish",
          quantity: 3,
          price: 200,
          discount: 0.2,
          details: [
            { productItemId: 32, warehouse: "Main Warehouse", location: "Aisle 11" },
            { productItemId: 33, warehouse: "Main Warehouse", location: "Aisle 11" },
            { productItemId: 34, warehouse: "Main Warehouse", location: "Aisle 11" }
          ]
        },
        {
          name: "Lemon",
          quantity: 5,
          price: 110.75,
          discount: 0.15,
          details: [
            { productItemId: 35, warehouse: "Main Warehouse", location: "Aisle 12" },
            { productItemId: 36, warehouse: "Main Warehouse", location: "Aisle 12" },
            { productItemId: 37, warehouse: "Main Warehouse", location: "Aisle 12" },
            { productItemId: 38, warehouse: "Main Warehouse", location: "Aisle 12" },
            { productItemId: 39, warehouse: "Main Warehouse", location: "Aisle 12" }
          ]
        }
      ]
    },
    {
      id: "ORD007",
      customer: "David Lee",
      total: 528.9,
      status: "Shipped",
      date: "2023-07-07",
      products: [
        {
          name: "Steak",
          quantity: 2,
          price: 400,
          discount: 0.3,
          details: [
            { productItemId: 40, warehouse: "Secondary Warehouse", location: "Aisle 13" },
            { productItemId: 41, warehouse: "Secondary Warehouse", location: "Aisle 13" }
          ]
        },
        {
          name: "Potatoes",
          quantity: 6,
          price: 128.9,
          discount: 0.1,
          details: [
            { productItemId: 42, warehouse: "Secondary Warehouse", location: "Aisle 14" },
            { productItemId: 43, warehouse: "Secondary Warehouse", location: "Aisle 14" },
            { productItemId: 44, warehouse: "Secondary Warehouse", location: "Aisle 14" },
            { productItemId: 45, warehouse: "Secondary Warehouse", location: "Aisle 14" },
            { productItemId: 46, warehouse: "Secondary Warehouse", location: "Aisle 14" },
            { productItemId: 47, warehouse: "Secondary Warehouse", location: "Aisle 14" }
          ]
        }
      ]
    },
    {
      id: "ORD008",
      customer: "Grace Taylor",
      total: 189.6,
      status: "Delivered",
      date: "2023-07-08",
      products: [
        {
          name: "Apples",
          quantity: 6,
          price: 100,
          discount: 0.05,
          details: [
            { productItemId: 48, warehouse: "Main Warehouse", location: "Aisle 15" },
            { productItemId: 49, warehouse: "Main Warehouse", location: "Aisle 15" },
            { productItemId: 50, warehouse: "Main Warehouse", location: "Aisle 15" },
            { productItemId: 51, warehouse: "Main Warehouse", location: "Aisle 15" },
            { productItemId: 52, warehouse: "Main Warehouse", location: "Aisle 15" },
            { productItemId: 53, warehouse: "Main Warehouse", location: "Aisle 15" }
          ]
        },
        {
          name: "Bananas",
          quantity: 5,
          price: 89.6,
          discount: 0.1,
          details: [
            { productItemId: 54, warehouse: "Main Warehouse", location: "Aisle 16" },
            { productItemId: 55, warehouse: "Main Warehouse", location: "Aisle 16" },
            { productItemId: 56, warehouse: "Main Warehouse", location: "Aisle 16" },
            { productItemId: 57, warehouse: "Main Warehouse", location: "Aisle 16" },
            { productItemId: 58, warehouse: "Main Warehouse", location: "Aisle 16" }
          ]
        }
      ]
    }
  ];

  const [orderData, setOrderStatus] = useState(initialOrderData);

  const markAsProcessing = (orderId) => {
    setOrderStatus(prevData => {
      return prevData.map(order => order.id === orderId ? { ...order, status: 'Processing' } : order);
    });
  };

  const markAsDelivered = (orderId) => {
    setOrderStatus(prevData => {
      return prevData.map(order => order.id === orderId ? { ...order, status: 'Delivered' } : order);
    });
  };
  const markAsCancelled = (orderId) => {
    setOrderStatus(prevData => {
      return prevData.map(order => order.id === orderId ? { ...order, status: 'Cancelled' } : order);
    });
  };


  return (
    <orderContext.Provider value={{ orderData,markAsCancelled,markAsProcessing, markAsDelivered }}>
      {children}
    </orderContext.Provider>
  );
};

const useOrder = () => {
  return useContext(orderContext);
};

export { useOrder, OrderProvider };
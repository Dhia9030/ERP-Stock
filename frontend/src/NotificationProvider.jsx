import React, { createContext, useEffect, useRef } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useProducts } from './ProductProvider';

export const NotificationContext = createContext();

const NotificationProvider = ({ children }) => {
  const PRODUCT_DATA = useProducts();
  const notifiedProducts = useRef(new Set());

  useEffect(() => {
    //console.log("PRODUCT_DATA:", PRODUCT_DATA); // Debugging log
    console.log("notifiedProducts (before):", notifiedProducts.current); // Debugging log


    if (PRODUCT_DATA && PRODUCT_DATA.length > 0) {
      PRODUCT_DATA.forEach((product) => {
        if (product.stock <= 5 && !notifiedProducts.current.has(product.id)) {
          console.log(`Low stock: ${product.name}`); // Debugging log
          toast.warn(`Low stock: ${product.name}`, {
            position: "top-right",
            autoClose: 5000, // Auto close after 5 seconds
          });
          notifiedProducts.current.add(product.id);
          console.log("notifiedProducts (after):", notifiedProducts.current); // Debugging log

        }
      });
    }
  }, [PRODUCT_DATA]);

  return (
    <NotificationContext.Provider value={{}}>
      {children}
      <ToastContainer />
    </NotificationContext.Provider>
  );
};

export  {NotificationProvider};
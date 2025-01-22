import {createContext , useContext} from 'react';


const productContext = createContext();

const ProductProvider=({children})=>{

    const products = [
      { id: 1, name: "Wireless Earbuds", category: "Electronics", price: 59.99, stock: 2, sales: 1200 },
      { id: 2, name: "Leather Wallet", category: "beauty", price: 39.99, stock: 89, sales: 800 },
      { id: 3, name: "Smart Watch", category: "Electronics", price: 199.99, stock: 3, sales: 650 },
      { id: 4, name: "Yoga Mat", category: "clothing", price: 29.99, stock: 4, sales: 950 },
      { id: 5, name: "Coffee Maker", category: "furniture", price: 79.99, stock: 78, sales: 720 },
    ];

    return(
        <productContext.Provider value={products}>
            {children}
        </productContext.Provider>
    )

}

const useProducts=()=>{
    return useContext(productContext);
}

export {ProductProvider, useProducts};
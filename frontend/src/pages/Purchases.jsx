import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Header from "../components/common/Header";
import DailyOrders from "../components/orders/DailyOrders";
import OrderDistribution from "../components/orders/OrderDistribution";
import OrdersTable from "../components/orders/OrdersTable";
import CustomerOrder from './CustomerOrder';
import {useOrder} from "../context/OrderProvider"


const Purchases = () => {
    
    

    return (
        <div className='flex-1 relative z-10 overflow-auto'>
            <Header title={"Purchases"} />
            
        </div>
    );
};

export default Purchases;
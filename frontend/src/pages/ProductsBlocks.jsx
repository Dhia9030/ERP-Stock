import React from 'react';
import ProductsInfo from '../components/productsBlocks/ProductsInfo.jsx';
import Header from '../components/common/Header';

const ProductsBlocks = () => {
    return (
        <div className="flex-1 overflow-auto relative z-10">
            <Header title="Products Information" />
            <div className="p-4">
                <ProductsInfo />
            </div>
        </div>
    );
};

export default ProductsBlocks;
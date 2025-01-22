import { useState, useEffect } from 'react';
import './Category.css';

const Category = ({selectedCategory, setSelectedCategory}) => {
  const [activeButton, setActiveButton] = useState(null);
  const categories = ['all', 'furniture', 'electronics', 'food', 'clothing', 'beauty'];

  const handleButtonClick = (index ) => {
    
    setActiveButton(index);
    setSelectedCategory(categories[index].toLowerCase()); // Changed to pass category in lowercase
  };

  return (
    <div className='text-center flex flex-col items-center justify-center'> {/* Added Tailwind CSS classes for styling */}
      <h1 className='text-center w-full title text-2xl font-bold mb-4'>Categories</h1> {/* Added Tailwind CSS classes for styling */}
      <div className="buttons-container">
        {categories.map((category, index) => (
          <div className="button-box" key={index}>
            <button
              className={` button gradient-button min-w-[210px] ${activeButton !== null && activeButton !== index ? 'bw-button' : ''}`}
              onClick={() => handleButtonClick(index)} /* Changed to pass category in lowercase */
            >
              {category.charAt(0).toUpperCase() + category.slice(1)}
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Category;
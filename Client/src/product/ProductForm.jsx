import React, { useState } from 'react';
import axios from 'axios';

const ProductForm = ({ onProductCreated }) => {
  const [product, setProduct] = useState({
    productName: '',
    price: 0,
    // Other product properties
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProduct((prevProduct) => ({
      ...prevProduct,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/Product`, product);
      onProductCreated(response.data);
      // Reset form
      setProduct({
        productName: '',
        price: 0,
      });
    } catch (error) {
      console.error('Error creating product:', error);
      // Handle error, display an error message, etc.
    }
  };

  return (
    <div>
      <h2>Create New Product</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Product Name:</label>
          <input type="text" name="productName" value={product.productName} onChange={handleChange} />
        </div>
        <div>
          <label>Price:</label>
          <input type="number" name="price" value={product.price} onChange={handleChange} />
        </div>
        {/* Add more input fields for other product properties */}
        <button type="submit">Create</button>
      </form>
    </div>
  );
};

export default ProductForm;

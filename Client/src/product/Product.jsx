import React, { useState, useEffect } from 'react';
import axios from 'axios';
import ProductForm from './ProductForm'
export {Product}
const Product = () => {
  const [products, setProducts] = useState([]);
  const [showForm, setShowForm] = useState(false);
  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/Product`);
      setProducts(response.data);
    } catch (error) {
      console.error('Error fetching products:', error);
      // Handle error, display an error message, etc.
    }
  };

  return (
    <div>
      <h2>Product List</h2>
      <button onClick={() => setShowForm(!showForm)}>Create New Product</button>
      {showForm && <ProductForm />}
      <ul>
        {products.map((product) => (
          <li key={product.productId}>{product.productName} - ${product.price}</li>
        ))}
      </ul>
    </div>
  );
};

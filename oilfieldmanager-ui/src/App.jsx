import { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';

function App() {
  const [assets, setAssets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Fetch the data from the .NET Web API when the page boots up
  useEffect(() => {
    axios.get('http://localhost:5185/api/assets')
      .then(response => {
        setAssets(response.data);
        setLoading(false);
      })
      .catch(err => {
        console.error("API Error: ", err);
        setError("Could not connect to the Oilfield Management API.");
        setLoading(false);
      });
  }, []);

  // Helper function to color-code equipment operational status badges
  const getStatusStyle = (status) => {
    switch (status) {
      case 'Active': return { backgroundColor: '#d4edda', color: '#155724', padding: '4px 8px', borderRadius: '4px', fontWeight: 'bold' };
      case 'Maintenance': return { backgroundColor: '#fff3cd', color: '#856404', padding: '4px 8px', borderRadius: '4px', fontWeight: 'bold' };
      case 'Inactive': return { backgroundColor: '#e2e3e5', color: '#383d41', padding: '4px 8px', borderRadius: '4px', fontWeight: 'bold' };
      default: return { backgroundColor: '#f8d7da', color: '#721c24', padding: '4px 8px', borderRadius: '4px', fontWeight: 'bold' };
    }
  };

  if (loading) return <div className="loading">Loading Oilfield Assets Engine...</div>;
  if (error) return <div className="error-banner">{error}</div>;

  return (
    <div className="dashboard-container">
      <header className="dashboard-header">
        <h1>🛠️ Operations Command Center</h1>
        <p className="subtitle">Enterprise Asset & Rig Management System</p>
      </header>

      <main className="dashboard-content">
        <div className="card">
          <h2>Heavy Downhole Equipment Inventory</h2>
          <table className="asset-table">
            <thead>
              <tr>
                <th>Serial Number</th>
                <th>Equipment Model</th>
                <th>Current Status</th>
                <th>Assigned Well Site</th>
              </tr>
            </thead>
            <tbody>
              {assets.map(asset => (
                <tr key={asset.id}>
                  <td className="serial">{asset.serialNumber}</td>
                  <td>{asset.model}</td>
                  <td>
                    <span style={getStatusStyle(asset.status)}>
                      {asset.status}
                    </span>
                  </td>
                  <td className="well-cell">
                    {asset.wellName ? `📍 ${asset.wellName}` : '🏢 Central Warehouse Warehouse'}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </main>
    </div>
  );
}

export default App;

import React, { useEffect, useState } from 'react';
import httpModule from '../../Helpers/http.module';

const MyComponent = ({ id }) => {
    const [data, setData] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await httpModule.get(`Results/applications/${id}`);
                const jsonData = await response.data;
                setData(jsonData);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
    }, [id]);

    return (
        <div>
            {data ? (
                <div>
                    <p>{data.result}</p>
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
};

export default MyComponent;

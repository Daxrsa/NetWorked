import React, { useEffect, useState } from "react";
import axios from "axios";

interface FetchReturn {
  data: any[];
  loading: boolean;
  error: boolean;
  reFetch: () => Promise<void>;
}

const useFetch = (url: string): FetchReturn => {
  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const res = await axios.get(url);
        setData(res.data);
      } catch (err: unknown) {
        setError(!!err); // Assuming you want to set `error` to `true` when there's an error
      }
      setLoading(false);
    };
    fetchData();
  }, [url]);

  const reFetch = async () => {
    setLoading(true);
    try {
      const res = await axios.get(url);
      setData(res.data);
    } catch (err: unknown) {
      setError(!!err);
    }
    setLoading(false);
  };

  return { data, loading, error, reFetch };
};

export default useFetch;

import React from "react";

const useCreateShortUrl = () => {
  const [shortUrl, setShortUrl] = React.useState<string>("");
  const [loading, setLoading] = React.useState<boolean>(false);

  const shortenRequest = async (inputData: string) => {
    const body = {
      longUrl: inputData,
    };
    setLoading(true);
    //const res = await fetch("http://localhost:5145/api/shorturls", {
    const res = await fetch("api/shorturls", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });
    setLoading(false);
    const resObj = await res.json();
    setShortUrl(resObj.shortUrl);
  };

  return { shortenRequest, shortUrl, setShortUrl, loading };
};

export default useCreateShortUrl;

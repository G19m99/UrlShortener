import { useMutation } from "@tanstack/react-query";
import { motion } from "framer-motion";
import React from "react";
import Loader from "./Loader";

type ShortenUrlFormProps = {
  setShortUrl: React.Dispatch<React.SetStateAction<string>>;
};

const ShortenUrlForm = ({ setShortUrl }: ShortenUrlFormProps) => {
  const baseUrl = "https://localhost:7164/";

  const shortenMutation = useMutation({
    mutationFn: async (longUrl: string) => {
      return fetch(`${baseUrl}api/urls`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ url: longUrl }),
      });
    },
    onSuccess: async (data) => {
      const res = await data.text();
      setShortUrl(res);
    },
    onError: (error) => {
      console.error("Error shortening URL:", error);
      alert("Failed to shorten URL. Please try again.");
    },
  });

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const formData = new FormData(e.currentTarget);
    const data = Object.fromEntries(formData.entries());
    shortenMutation.mutate(data.longUrl as string);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label
          htmlFor="longUrl"
          className="block text-sm font-medium text-slate-700 mb-1"
        >
          Long URL
        </label>
        <input
          type="url"
          id="longUrl"
          name="longUrl"
          placeholder="https://example.com/very/long/url/that/needs/shortening"
          required
          className="w-full px-4 py-2 border border-slate-300 rounded-lg focus:ring-2 focus:ring-violet-500 focus:border-violet-500 outline-none transition-all"
        />
      </div>

      <div>
        <label
          htmlFor="baseUrl"
          className="block text-sm font-medium text-slate-700 mb-1"
        >
          Base URL
        </label>
        <input
          type="text"
          name="baseUrl"
          id="baseUrl"
          value={baseUrl}
          readOnly
          className="w-full px-4 py-2 border border-slate-300 rounded-lg bg-slate-50 text-slate-500 cursor-not-allowed"
        />
        <p className="mt-1 text-xs text-slate-500">
          This field is currently locked
        </p>
      </div>

      <motion.button
        type="submit"
        className="w-full bg-violet-600 hover:bg-violet-700 text-white font-medium py-2 px-4 rounded-lg transition-colors"
        whileHover={{ scale: 1.02 }}
        whileTap={{ scale: 0.98 }}
        disabled={shortenMutation.isPending}
      >
        {shortenMutation.isPending ? <Loader /> : "Shorten URL"}
      </motion.button>
    </form>
  );
};

export default ShortenUrlForm;

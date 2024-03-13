import React from "react";

interface CardProps {
  getShortUrlReq: (inputData: string) => void;
}

const Card = ({ getShortUrlReq }: CardProps) => {
  const handleSubmit = (e: React.SyntheticEvent) => {
    e.preventDefault();
    const target = e.target as typeof e.target & {
      url: { value: string };
    };
    getShortUrlReq(target.url.value);
  };

  return (
    <div>
      <form
        className="mx-auto mt-10 flex flex-col justify-center items-center gap-y-4 max-w-md gap-x-4 sm:flex-row"
        onSubmit={handleSubmit}
      >
        <label htmlFor="long-url" className="sr-only">
          Email address
        </label>
        <input
          id="long-url"
          name="url"
          type="text"
          required={true}
          className="min-w-full flex-auto rounded-md border-0 bg-white/5 px-3.5 py-2 text-white shadow-sm ring-1 ring-inset ring-white/10 focus:ring-2 focus:ring-inset focus:ring-white sm:text-sm sm:leading-6"
          placeholder="Enter your long URL"
        />

        <button
          type="submit"
          className="flex-none max-w-[250px] rounded-md bg-white px-3.5 py-2.5 text-sm font-semibold text-gray-900 shadow-sm hover:bg-gray-100 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-white"
        >
          Shorten me
        </button>
      </form>
    </div>
  );
};

export default Card;

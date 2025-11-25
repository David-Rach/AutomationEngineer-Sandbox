import { useState } from 'react'

export function Counter() {
    const [count, setCount] = useState(0)

    function increment() {
        setCount(count + 1)
    }

    function decrement() {
        setCount(count - 1)
    }

    return (
        <div className="flex flex-col items-center gap-4">
            <p data-testid="count-value">Count: {count}</p>

            <div className="flex gap-2">
                <button onClick={increment} className="bg-blue-600 text-white px-3 py-1 rounded">
                    Increment
                </button>

                <button onClick={decrement} className="bg-red-600 text-white px-3 py-1 rounded">
                    Decrement
                </button>
            </div>
        </div>
    )
}

# Maximum Income from Asset Trading

## Problem Statement
Calculate the maximum income from trading an asset over N days, given:
- Start with one asset
- Can hold at most one asset at a time
- Can sell whenever holding an asset
- Can always afford to buy when not holding (infinite money)
- Return result modulo 1,000,000,000

## Solution Strategy

### Key Insight
The optimal strategy is to capture every profitable segment:
- **Sell before every price drop** (A[i] > A[i+1])
- **Buy before every price rise** (A[i] < A[i+1])
- **Sell on the last day** if still holding

This greedy approach maximizes profit by avoiding losses and capturing all gains.

### Algorithm
```
1. Start holding one asset
2. For each day i (except last):
   - If holding and price drops tomorrow: SELL today
   - If not holding and price rises tomorrow: BUY today
3. If still holding on last day: SELL
4. Return income % 1,000,000,000
```

### Complexity
- **Time**: O(N) - single pass through array
- **Space**: O(1) - only tracking income and holding state

## Implementation

```csharp
public class Solution
{
    public int solution(int[] A)
    {
        const long MOD = 1_000_000_000;
        int n = A.Length;
        
        long income = 0;
        bool holding = true; // Start with one asset
        
        for (int i = 0; i < n - 1; i++)
        {
            if (holding)
            {
                // Sell before a drop
                if (A[i] > A[i + 1])
                {
                    income = (income + A[i]) % MOD;
                    holding = false;
                }
            }
            else
            {
                // Buy before a rise
                if (A[i] < A[i + 1])
                {
                    income = (income - A[i] % MOD + MOD) % MOD;
                    holding = true;
                }
            }
        }
        
        // Sell on last day if still holding
        if (holding)
        {
            income = (income + A[n - 1]) % MOD;
        }
        
        return (int)income;
    }
}
```

## Test Cases

### Test 1: Basic Trading
```csharp
Input:  [4, 1, 2, 3]
Output: 6

Explanation:
- Day 0: Sell at 4 (income = 4, not holding)
- Day 1: Buy at 1 (income = 3, holding)
- Day 2: Hold (price rising)
- Day 3: Sell at 3 (income = 6)
Result: 4 - 1 + 3 = 6
```

### Test 2: Multiple Trades
```csharp
Input:  [1, 2, 3, 3, 2, 1, 5]
Output: 7

Explanation:
- Day 0-2: Hold through rise (1→2→3)
- Day 3: Sell at 3 (income = 3, not holding)
- Day 4-5: Wait through drop (3→2→1)
- Day 5: Buy at 1 (income = 2, holding)
- Day 6: Sell at 5 (income = 7)
Result: 3 - 1 + 5 = 7
```

### Test 3: Large Numbers with Modulo
```csharp
Input:  [1000000000, 1, 2, 2, 1000000000, 1, 1000000000]
Output: 999999998

Explanation:
- Sell at 1000000000, buy at 1, sell at 2
- Buy at 2, sell at 1000000000
- Buy at 1, sell at 1000000000
Total: 1000000000 - 1 + 2 - 2 + 1000000000 - 1 + 1000000000 = 2999999998
Result: 2999999998 % 1000000000 = 999999998
```

## Edge Cases Handled

1. **Single day**: Sell immediately (return A[0] % MOD)
2. **Monotonic increase**: Hold until last day, sell once
3. **Monotonic decrease**: Sell on day 0, never buy back
4. **Plateaus**: Hold through equal prices, only act on changes
5. **Large numbers**: Proper modulo arithmetic prevents overflow

## Modulo Arithmetic Notes

```csharp
// When subtracting (buying), ensure non-negative result:
income = (income - A[i] % MOD + MOD) % MOD;

// This handles cases where A[i] % MOD might be larger than current income
```

## Running Tests

```bash
dotnet run MaxIncomeTest.cs MaxIncome.cs
```

Expected output:
```
Test 1: 6 (expected 6)
Test 2: 7 (expected 7)
Test 3: 999999998 (expected 999999998)
```

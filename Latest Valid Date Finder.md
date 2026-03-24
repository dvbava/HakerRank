# Latest Valid Date Finder

## Problem Statement
Given a date string in "MM-DD" format with some digits replaced by "?", find the latest possible valid date by replacing question marks with digits (0-9).

### Constraints
- Date format: "MM-DD" (5 characters, third is "-")
- Months: 01-12
- Days per month: Jan(31), Feb(28), Mar(31), Apr(30), May(31), Jun(30), Jul(31), Aug(31), Sep(30), Oct(31), Nov(30), Dec(31)
- Return "xx-xx" if no valid date exists

## Solution Strategy

### Approach
Brute force from latest to earliest:
1. Start with December 31 (12-31)
2. Try each valid date in descending order
3. Check if the candidate matches the pattern (? matches any digit)
4. Return first match, or "xx-xx" if none found

### Why This Works
- Focus on correctness over performance (as specified)
- Guarantees finding the latest valid date
- Simple pattern matching logic
- No complex edge case handling needed

## Implementation

```csharp
public class Solution
{
    private static readonly int[] daysInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    
    public string solution(string date)
    {
        // Try all possible combinations from latest to earliest
        for (int month = 12; month >= 1; month--)
        {
            for (int day = daysInMonth[month]; day >= 1; day--)
            {
                string candidate = $"{month:D2}-{day:D2}";
                if (Matches(date, candidate))
                {
                    return candidate;
                }
            }
        }
        
        return "xx-xx";
    }
    
    private bool Matches(string pattern, string candidate)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] != '?' && pattern[i] != candidate[i])
            {
                return false;
            }
        }
        return true;
    }
}
```

### Key Components

1. **daysInMonth array**: Maps month number to max days (index 0 unused for clarity)
2. **Nested loops**: Iterate months 12→1, days max→1
3. **Matches helper**: Checks if pattern matches candidate (? is wildcard)

## Test Cases

### Test 1: Ambiguous Month
```csharp
Input:  "?1-31"
Output: "01-31"

Explanation:
- Possible months: 01 (January) or 11 (November)
- Only January has 31 days
- November has 30 days (invalid)
```

### Test 2: Fixed Month, Variable Day
```csharp
Input:  "02-??"
Output: "02-28"

Explanation:
- Month is fixed: 02 (February)
- February has 28 days
- Latest valid day: 28
```

### Test 3: Impossible Date
```csharp
Input:  "??-4?"
Output: "xx-xx"

Explanation:
- Day must be 40-49
- No month has 40+ days
- No valid date exists
```

### Test 4: Invalid Fixed Date
```csharp
Input:  "09-31"
Output: "xx-xx"

Explanation:
- September (09) has only 30 days
- Day 31 is invalid for September
```

### Test 5: All Wildcards
```csharp
Input:  "??-??"
Output: "12-31"

Explanation:
- All digits are wildcards
- Latest possible date: December 31
```

### Test 6: Fixed Month, Variable Day
```csharp
Input:  "01-??"
Output: "01-31"

Explanation:
- Month is fixed: 01 (January)
- January has 31 days
- Latest valid day: 31
```

## Algorithm Analysis

### Time Complexity
- Worst case: O(12 × 31 × 5) = O(1860) = O(1) constant time
- At most 372 date candidates (sum of days in all months)
- Each pattern match is O(5) = O(1)

### Space Complexity
- O(1) - only stores constant-size variables and array

### Correctness
- Exhaustive search guarantees finding the latest valid date
- Pattern matching handles all wildcard combinations
- Returns "xx-xx" when no valid date exists

## Edge Cases Handled

1. **All wildcards** ("??-??"): Returns 12-31
2. **No wildcards** ("09-31"): Validates fixed date
3. **Partial wildcards** ("?1-31"): Finds latest matching month
4. **Impossible dates** ("??-4?"): Returns "xx-xx"
5. **Month boundaries**: Correctly handles different days per month

## Running Tests

```bash
dotnet run --project LatestDate.csproj
```

Expected output:
```
Test 1: 01-31 (expected 01-31)
Test 2: 02-28 (expected 02-28)
Test 3: xx-xx (expected xx-xx)
Test 4: xx-xx (expected xx-xx)
Test 5: 12-31 (expected 12-31)
Test 6: 01-31 (expected 01-31)
```

## Alternative Approaches (Not Implemented)

### Greedy Digit-by-Digit
- Replace each ? with largest valid digit
- More complex logic for interdependent constraints
- Harder to reason about correctness

### Dynamic Programming
- Overkill for this problem size
- No overlapping subproblems to exploit

### Regex/Pattern Matching
- Could generate regex from pattern
- More complex implementation
- No performance benefit for small input

## Conclusion
The brute force approach is optimal for this problem given:
- Small constant search space (≤372 candidates)
- Correctness prioritized over performance
- Simple, maintainable code
